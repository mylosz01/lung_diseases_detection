# Import biblotek
import os
import librosa
import numpy as np
import tensorflow as tf
from tensorflow.keras.preprocessing.image import load_img, img_to_array
from sklearn.model_selection import train_test_split
from tensorflow.keras.models import Sequential
from tensorflow.keras import layers
from tensorflow.keras import Input
from tensorflow.keras.models import Model
from tensorflow.keras.callbacks import ModelCheckpoint, ReduceLROnPlateau, EarlyStopping
from tensorflow.keras.utils import to_categorical
from tensorflow.keras.layers import Dense, Flatten, Dropout, Activation, LSTM, SimpleRNN, Conv1D, Input, BatchNormalization


# Stałe wartosci
BASE_PATH = './data/spectograms'
INPUT_SHAPE = (224,224)


def load_and_split_data(target_size = (224,224)):

    spectogram_array = []
    spectogram_labels = []
    first = False

    class_names = os.listdir(f'{BASE_PATH}')
    print(class_names)

    for class_number, name in enumerate(class_names):
        spect_files = os.listdir(f"{BASE_PATH}/{name}")

        for spectogram in spect_files:
            img_path = os.path.join(BASE_PATH,name,spectogram)
            
            img = load_img(img_path,target_size=INPUT_SHAPE)
            img_array = img_to_array(img)

            spectogram_array.append(img_array)
            spectogram_labels.append(class_number)

    spectogram_array = np.array(spectogram_array)
    spectogram_labels = np.array(spectogram_labels)

    # Podział danych na testowe oraz treningowe
    X_train, X_test, y_train, y_test = train_test_split(spectogram_array,spectogram_labels,test_size=0.3,random_state=42)

    # Sprawdzenie rozmiarów zbiorów danych
    print("Rozmiar zbioru treningowego:", X_train.shape)
    print("Rozmiar zbioru testowego:", X_test.shape)

    return X_train, X_test, y_train, y_test, len(class_names)


def create_model(target_shape=INPUT_SHAPE,output_size=2):
    model = tf.keras.models.Sequential()
    model.add(layers.Conv2D(8, (3, 3), padding='same',activation='relu', input_shape = (target_shape[0],target_shape[1],3) ))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Conv2D(16, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Conv2D(32, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Conv2D(64, (3, 3), padding='same',activation='relu'))
    model.add(layers.MaxPooling2D(pool_size=(2,2)))
    model.add(layers.Flatten())
    model.add(layers.Dense(512, activation='relu'))
    model.add(layers.Dense(256, activation='relu'))
    model.add(layers.Dense(output_size, activation='softmax')) # Warstwa wyjściowa

    model.compile(
        optimizer = tf.keras.optimizers.Adam(learning_rate = 0.001),
        loss = tf.keras.losses.CategoricalCrossentropy(),
        metrics = ['accuracy',tf.keras.metrics.Recall(),tf.keras.metrics.Precision()]
    )

    return model


if __name__ == "__main__":

    # Wczytanie danych
    X_train, X_test, y_train, y_test, number_of_classes = load_and_split_data(target_size = INPUT_SHAPE)

    # Tworzenie modelu
    cnn_model = create_model(target_shape = INPUT_SHAPE, output_size = number_of_classes)

    #Wyświetalanie podsumowania modelu
    #cnn_model.summary()


# Trenowanie modelu



# Ewaluacja modelu



# Przykładowa predykcja




# Zapis modelu


