using Configs;
using MusicalRepresentation;

public class MusicCore : UnitySingleton<MusicCore>
{
    public float leadTime = .001f;
    public float trailTime = .002f;
    public float measureLength = 5f;

    private Score _score;
        
    public void RegisterScore(Score score)
    {
        _score = score;
    }

    public void InitScore(SongConfig config)
    {
        measureLength = config.measureLength;
        _score.Init(config);
    }

    public void RecordHit(Instrument instrument, float absoluteTime)
    {
        _score.RecordHit(instrument, absoluteTime);
    }
    
    public static bool DoNotesOverlap(Note a, Note b)
    {
        return (a.HitTime + MusicCore.Instance.trailTime) >= (b.HitTime - MusicCore.Instance.leadTime) &&
               (b.HitTime + MusicCore.Instance.trailTime) >= (a.HitTime - MusicCore.Instance.leadTime);
    }

    public static bool IsHit(Note a, float relativeTime)
    {
        return ((a.HitTime - MusicCore.Instance.leadTime) < relativeTime) &&
            (relativeTime < (a.HitTime + MusicCore.Instance.trailTime));
    }
}