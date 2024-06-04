import os
from datetime import datetime
import librosa
import numpy as np
import tensorflow as tf
import matplotlib.pyplot as plt
import seaborn as sns
from tensorflow.keras.preprocessing.image import load_img, img_to_array
from sklearn.model_selection import train_test_split
from tensorflow.keras import layers
from tensorflow.keras import Input
from tensorflow.keras.models import load_model

import audio_to_spectrogram as _audio_

DISEASE_LIST = ['Asthma','Bronchiectasis','Bronchiolitis','COPD','Covid-19','Healthy','Heart_Failure','LRTI','Pneumonia','Symptomatic','URTI']

#PATH_TO_MODEL = os.path.join("Model","model_08_05_2024-13_...keras")
PATH_TO_MODEL = os.path.join("./models","model_04_06_2024-10_21__2__0.7491.keras")
PATH_TO_FILE = os.path.join("./data","audio","COPD","107_2b3_Ar_mc_AKGC417L.wav")

class Predict_Diseases:

    model = None
    prediction = None
    spectograms = []
    dataset = None

    def __init__(self,):
        self.load_model_from_file()

    def load_model_from_file(self):
        self.model = load_model(PATH_TO_MODEL, compile=True)

    def prepare_data(self,spect_path):
        array = _audio_.get_mel_spectograms(filepath = spect_path, sample_rate = 24000)
        #print(len(array))
        self.dataset = tf.convert_to_tensor(array, dtype=tf.float32)

    def predict_result(self):
        self.prediction = self.model.predict(self.dataset, verbose=0)


if __name__ == "__main__":

    # Stworzenie obiektu predykcji
    predict_disease = Predict_Diseases()

    #Wczytanie audio
    predict_disease.prepare_data(spect_path=PATH_TO_FILE)

    # Predykcja
    predict_disease.predict_result()

    val = set(np.argmax(predict_disease.prediction, axis=1))
    #print(val)
    for i in val:
        print(f"{DISEASE_LIST[i]}")