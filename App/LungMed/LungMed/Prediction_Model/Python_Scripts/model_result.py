#imports
import numpy as np
import os
from tensorflow.keras.models import load_model

PATH_TO_MODEL = os.path.join("Model","model_08_05_2024-13_...keras")

def load_model_from_file():
    model = load_model(PATH_TO_MODEL,compile = True)
    return model
    
    
def test_model(model,X_test,y_test):

    y_pred = np.argmax(model.predict(X_test), axis= 1)
    
    test_acc = sum(y_pred == np.argmax(y_test, axis= 1)) / len(y_test)
    
    print(f"Accuracy model: {np.round(test_acc * 100,2)} %")
    
    return y_pred, test_acc

def predict_for_single_value(model,spectrogram):
    spectrogram_as_list = [spectrogram]
    prediction = np.argmax(model.predict(spectrogram_as_list),axis=1) #spytać jak przerobić predict dla pojedynczej wartosci
    return prediction
    
    
    
