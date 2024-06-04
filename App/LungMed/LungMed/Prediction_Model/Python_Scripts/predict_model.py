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

DISEASE_LIST = ['Asthma','Bronchiectasis','Bronchiolitis','COPD','Covid-19','Healthy','Heart_Failure','LRTI','Pneumonia','Symptomatic','URTI']

#PATH_TO_MODEL = os.path.join("Model","model_08_05_2024-13_...keras")
PATH_TO_MODEL = os.path.join("./models","model_04_06_2024-10_21__2__0.7491.keras")
PATH_TO_FILE = os.path.join("./data","spectograms","Healthy","0a8e4279-3840-4872-9db8-3b5d8c53cc4_0.png")

class Predict_Diseases:

    model = None
    prediction = None
    spectograms = []
    dataset = None

    def __init__(self,):
        self.load_model_from_file()

    def load_model_from_file(self):
        self.model = load_model(PATH_TO_MODEL, compile=True)

    def load_audio_file(self):
        pass

    def prepare_data(self,spect_path):

        def load_and_preprocess_image(file_path):
            img = tf.io.read_file(file_path)
            img = tf.image.decode_image(img, channels=3, expand_animations=False)
            img = tf.image.resize(img, [224, 224])
            img = img / 255.0
            img = tf.expand_dims(img, axis=0)

            return img

        self.dataset = load_and_preprocess_image(PATH_TO_FILE)

    def predict_result(self):
        self.prediction = self.model.predict(self.dataset)


if __name__ == "__main__":

    # Stworzenie obiektu predykcji
    predict_disease = Predict_Diseases()

    #Wczytanie audio
    predict_disease.prepare_data(spect_path=PATH_TO_FILE)

    # Predykcja
    predict_disease.predict_result()

    val = np.argmax(predict_disease.prediction, axis=1)[0]
    print(f"Prediction : {DISEASE_LIST[val]}")