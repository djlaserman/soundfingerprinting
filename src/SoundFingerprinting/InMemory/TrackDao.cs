﻿namespace SoundFingerprinting.InMemory
{
    using System.Collections.Generic;
    using System.Linq;

    using SoundFingerprinting.DAO;
    using SoundFingerprinting.DAO.Data;
    using SoundFingerprinting.Data;

    internal class TrackDao : ITrackDao
    {
        private readonly IRAMStorage storage;

        public TrackDao(IRAMStorage storage)
        {
            this.storage = storage;
        }

        public int Count => storage.Tracks.Count;

        public TrackData InsertTrack(TrackInfo track)
        {
            return storage.AddTrack(track);
        }

        public void InsertTrack(TrackData track)
        {
            storage.AddTrack(track);
        }

        public TrackData ReadTrackById(string id)
        {
            return storage.Tracks.FirstOrDefault(pair => pair.Value.ISRC == id).Value;
        }

        public IEnumerable<TrackData> ReadAll()
        {
            return storage.Tracks.Values;
        }

        public IEnumerable<TrackData> ReadTrackByTitle(string title)
        {
            return storage.Tracks
                .Where(pair => pair.Value.Title == title)
                .Select(pair => pair.Value);
        }

        public TrackData ReadTrack(IModelReference trackReference)
        {
            if (storage.Tracks.ContainsKey(trackReference))
            {
                return storage.Tracks[trackReference];
            }

            return null;
        }

        public IEnumerable<TrackData> ReadTracksByReferences(IEnumerable<IModelReference> references)
        {
            return references.Aggregate(new List<TrackData>(), (list, reference) =>
            {
                if (storage.Tracks.TryGetValue(reference, out var track))
                {
                    list.Add(track);
                }

                return list;
            });
        }

        public int DeleteTrack(IModelReference trackReference)
        {
            return storage.DeleteTrack(trackReference);
        }
    }
}
