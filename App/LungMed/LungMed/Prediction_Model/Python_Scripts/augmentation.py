#imports
import numpy as np
import librosa
import noisereduce as nr

def preprocess_augmentation(chunks):
    augmented_chunks = []
    for chunk in chunks:
        aug_ch = add_white_noise(signal = chunk.copy(), noise_factor = 0.02)
        augmented_chunks.append(aug_ch)

        aug_ch = time_stretch(chunk.copy(), stretch_rate = 0.9)
        augmented_chunks.append(aug_ch)

        aug_ch = time_stretch(chunk.copy(), stretch_rate = 1.05)
        augmented_chunks.append(aug_ch)

        aug_ch = pitch_scale(signal = chunk.copy(), sr = 24000, num_semitones = 12)
        augmented_chunks.append(aug_ch)

        aug_ch = reduce_noise(signal = chunk.copy(), strength = 0.5)
        augmented_chunks.append(aug_ch)

        aug_ch = random_gain(signal = chunk.copy(), min_gain_factor = 2, max_gain_factor = 4)
        augmented_chunks.append(aug_ch)

    return augmented_chunks


def add_white_noise(signal, noise_factor = 0.01):
    noise = np.random.normal(0, signal.std(), signal.size)
    augmented_signal = signal + noise * noise_factor
    return np.array(augmented_signal)


def time_stretch(signal, stretch_rate = 0.8):
    return np.array(librosa.effects.time_stretch(y = signal, rate = stretch_rate))

def pitch_scale(signal, sr, num_semitones = 12):
    return np.array(librosa.effects.pitch_shift(y = signal, sr = sr, n_steps = num_semitones))

def reduce_noise(signal, strength = 1):
    return np.array(nr.reduce_noise(y = signal, sr = 24000 , n_std_thresh_stationary = strength, stationary=True))

def random_gain(signal,min_gain_factor = 2, max_gain_factor = 4):
    gain_factor = np.random.uniform(min_gain_factor,max_gain_factor)
    return np.array(signal * gain_factor)

