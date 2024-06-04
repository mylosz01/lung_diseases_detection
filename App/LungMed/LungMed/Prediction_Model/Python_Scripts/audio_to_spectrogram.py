#imports
import sys
import os
import shutil
import librosa
import glob
import numpy as np
import matplotlib.pyplot as plt

def get_mel_spectograms(filepath,sample_rate=22000,image_shape=(224,224)):
    
    SAMPLE_RATE = 24000
    CHUNK_LENGTH = 3  # w sekundach

    #Wczytanie pliku audio
    wave, rate = librosa.load(path = filepath, sr = sample_rate)

    #Podział dźwięku na odcinki
    signal_splits = []

    for i in range(0,len(wave),int(CHUNK_LENGTH * SAMPLE_RATE)):
        chunk = wave[i:i + int(CHUNK_LENGTH * SAMPLE_RATE)]

        if len(chunk) < int(CHUNK_LENGTH * SAMPLE_RATE):
            break
        
        signal_splits.append(np.array(chunk))

    #Tworzenie mel spektogramu dla kazdego z odcinka
    ready_mel_spectograms = []

    for chunk in signal_splits:

        mel_spectogram = librosa.feature.melspectrogram(y = chunk,
                                                        sr = rate,
                                                        n_fft = 1024,
                                                        hop_length = 512,
                                                        n_mels = image_shape[0] * 1,
                                                        fmin = 100,
                                                        fmax = 24000)
        
        mel_spectogram = librosa.amplitude_to_db(mel_spectogram, ref = np.max)

        #Normalizacja
        mel_spectogram -= mel_spectogram.min()
        mel_spectogram /= mel_spectogram.max()

        #mel_spectogram = np.resize(mel_spectogram,(image_shape[0],image_shape[1]))
        mel_spectogram = np.resize(mel_spectogram,(image_shape[0],image_shape[1],3))

        #Dodanie melspektogramu do listy
        ready_mel_spectograms.append(np.array(mel_spectogram))

    return ready_mel_spectograms
