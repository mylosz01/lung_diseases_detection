# Import biblotek
import os
from datetime import date
import librosa
import numpy as np
import tensorflow as tf
import matplotlib.pyplot as plt
import seaborn as sns
from tensorflow.keras.preprocessing.image import load_img, img_to_array
from sklearn.model_selection import train_test_split
from tensorflow.keras import layers
from tensorflow.keras import Input

# Stałe wartosci
BASE_PATH = './data/spectograms/'
INPUT_SHAPE = (224,224)

def load_and_split_data(target_size = (224,224)):

    spectogram_array = []
    spectogram_labels = []

    class_names = os.listdir(f'{BASE_PATH}')
    print(class_names)

    for class_number, name in enumerate(class_names):
        spect_files = os.listdir(f"{BASE_PATH}/{name}")

        for spectogram in spect_files:
            img_path = os.path.join(BASE_PATH,name,spectogram)
            
            img = load_img(img_path,target_size=INPUT_SHAPE)
            img_array = img_to_array(img, dtype = np.float16)

            spectogram_array.append(img_array)
            spectogram_labels.append(class_number)

    spectogram_array = np.array(spectogram_array, dtype = np.float16)
    spectogram_labels = np.array(spectogram_labels, dtype = np.float16)

    # Podział danych na testowe oraz treningowe
    X_train, X_test, y_train, y_test = train_test_split(spectogram_array,spectogram_labels,test_size=0.3,random_state=42)

    # Sprawdzenie rozmiarów zbiorów danych
    print("Rozmiar zbioru treningowego:", X_train.shape)
    print("Rozmiar zbioru testowego:", X_test.shape)
    print("Ilość rozpatrywanych klas : ", len(class_names))

    # Zmiana numerów labels na wektory
    y_train = tf.keras.utils.to_categorical(y_train, num_classes = len(class_names))
    y_test = tf.keras.utils.to_categorical(y_test, num_classes = len(class_names))

    return X_train, X_test, y_train, y_test, len(class_names), class_names


def create_model(target_shape=INPUT_SHAPE,output_size=2):
    model = tf.keras.models.Sequential()
    model.add( Input(shape = (target_shape[0],target_shape[1],3)))
    model.add(layers.Conv2D(8, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Conv2D(16, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Conv2D(32, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Conv2D(32, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Flatten())
    model.add(layers.Dense(512, activation='relu'))
    model.add(layers.Dropout(0.5))
    model.add(layers.Dense(256, activation='relu'))
    model.add(layers.Dropout(0.3))
    model.add(layers.Dense(units = output_size, activation='softmax')) # Warstwa wyjściowa

    model.compile(
        optimizer = tf.keras.optimizers.Adam(learning_rate = 0.001),
        loss = tf.keras.losses.CategoricalCrossentropy(),
        metrics = ['accuracy',tf.keras.metrics.Recall(),tf.keras.metrics.Precision()]
    )

    return model


def show_results(hist=None):

    plt.figure(figsize=(15,12))
    # Accuracy
    plt.subplot(2,2,1)
    plt.title("Accuracy")
    plt.plot(hist.history['accuracy'],'r',label="accuracy")
    plt.plot(hist.history['val_accuracy'],'b',label="val_accuracy")
    plt.xlabel("Epoch #")
    plt.ylabel("Accuracy")
    plt.legend()

    # Loss
    plt.subplot(2,2,2)
    plt.title("Loss")
    plt.plot(hist.history['loss'],'r',label="loss")
    plt.plot(hist.history['val_loss'],'b',label="val_loss")
    plt.xlabel("Epoch #")
    plt.ylabel("Loss")
    plt.legend()

    # Precision
    plt.subplot(2,2,3)
    plt.title("Precision")
    plt.plot(hist.history['precision'],'r',label="precision")
    plt.plot(hist.history['val_precision'],'b',label="val_precision")
    plt.xlabel("Epoch #")
    plt.ylabel("Precision")
    plt.legend()

    # Recall
    plt.subplot(2,2,4)
    plt.title("Recall")
    plt.plot(hist.history['recall'],'r',label="recall")
    plt.plot(hist.history['val_recall'],'b',label="val_recall")
    plt.xlabel("Epoch #")
    plt.ylabel("Recall")
    plt.legend()
        
    plt.show()


def test_model(model,X_test,y_test):

    y_pred = np.argmax(model.predict(X_test), axis= 1)

    test_acc = sum(y_pred == np.argmax(y_test, axis= 1)) / len(y_test)

    print(f"Accuracy model: {np.round(test_acc * 100,2)} %")

    return y_pred, test_acc


def show_confusion_matrix(y_true, y_pred, class_names):
    confusion_mtx = tf.math.confusion_matrix(np.argmax(y_true, axis = 1), y_pred)

    plt.figure(figsize=(10, 6))
    sns.heatmap(confusion_mtx,
                xticklabels=class_names,
                yticklabels=class_names,
                annot=True, fmt='g')
    plt.xlabel('Prediction')
    plt.ylabel('Label')
    plt.show()


if __name__ == "__main__":

    # Wczytanie danych
    X_train, X_test, y_train, y_test, num_of_classes, class_names = load_and_split_data(target_size = INPUT_SHAPE)
    print("SPECTOGRAMS IMAGES LOADED...")

    # Tworzenie modelu
    cnn_model = create_model(target_shape = INPUT_SHAPE, output_size = num_of_classes)

    #Wyświetalanie podsumowania modelu
    #cnn_model.summary()

    # Trenowanie modelu
    TRAIN_EPOCHS = 2
    BATCH_SIZE = 3

    train_history = cnn_model.fit(x = X_train,
                                  y = y_train,
                                  batch_size = BATCH_SIZE,
                                  epochs = TRAIN_EPOCHS,
                                  validation_data = (X_test,y_test))

    # Wyświetlanie wyników treningu
    show_results(train_history)

    # Ewaluacja modelu
    y_pred, result_acc = test_model(cnn_model,X_test,y_test)

    # Wyświetlenie confision_matrix
    show_confusion_matrix(y_test,y_pred,class_names)

    # Przykładowa predykcja


    # Zapis modelu
    now = date.today()
    current_time = now.strftime("%d_%m_%Y-%H_%M")
    cnn_model.save(f"./models/model_{current_time}__{TRAIN_EPOCHS}__{result_acc:.04f}.keras")
    print("Model saved...")
