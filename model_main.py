# Import biblotek
import os
import librosa
import numpy as np
import tensorflow as tf
from tensorflow.keras.preprocessing.image import load_img, img_to_array
from sklearn.model_selection import train_test_split

BASE_PATH = './data/spectograms'

def load_and_split_data():

    spectogram_array = []
    spectogram_labels = []
    first = False

    class_names = os.listdir(f'{BASE_PATH}')
    print(class_names)

    for class_number, name in enumerate(class_names):
        spect_files = os.listdir(f"{BASE_PATH}/{name}")

        for spectogram in spect_files:
            img_path = os.path.join(BASE_PATH,name,spectogram)
            
            img = load_img(img_path,target_size=(224,224))
            img_array = img_to_array(img)

            spectogram_array.append(img_array)
            spectogram_labels.append(class_number)

    spectogram_array = np.array(spectogram_array)
    spectogram_labels = np.array(spectogram_labels)

    X_train, X_test, y_train, y_test = train_test_split(spectogram_array,spectogram_labels,test_size=0.3,random_state=42)

    # Sprawdzenie rozmiarów zbiorów danych
    print("Rozmiar zbioru treningowego:", X_train.shape)
    print("Rozmiar zbioru testowego:", X_test.shape)

    return X_train, X_test, y_train, y_test



if __name__ == "__main__":
    # Wczytanie danych
    load_and_split_data()





# Tworzenie modelu



# Trenowanie modelu



# Ewaluacja modelu



# Przykładowa predykcja




# Zapis modelu


