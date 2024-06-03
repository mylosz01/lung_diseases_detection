#imports
from audio_to_spectrogram import SAMPLE_RATE
from audio_to_spectrogram import load_audio_path,get_mel_spectograms
from model_result import load_model_from_file, predict_for_single_value

import os

if __name__ == "__main__":
    audioPath = load_audio_path()
    print("aaa")
    #audioPath = os.path.join("Audio","101_1b1_Al_sc_Meditron.wav")
    
    mel_spectogram = get_mel_spectograms(audioPath,sample_rate=SAMPLE_RATE,use_augmentation=True)
    
    model = load_model_from_file()
    
    prediction = predict_for_single_value(model,mel_spectogram)
    
    #debug
    #print(prediction)
    #print(type(prediction))
    
    print(prediction)
    
    
    