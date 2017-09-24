﻿namespace SoundFingerprinting.Utils
{
    internal class FastFingerprintDescriptor : FingerprintDescriptor
    {
        private readonly QuickSelectAlgorithm quickSelect = new QuickSelectAlgorithm();

        public override bool[] ExtractTopWavelets(float[] frames, int topWavelets)
        {
            int[] indexes = RangeUtils.GetRange(frames.Length);
            quickSelect.Find(topWavelets - 1, frames, indexes, 0, frames.Length - 1);
            return EncodeFingerprint(frames, indexes, topWavelets);
        }
    }
}