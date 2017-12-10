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

    public void RecordHit(Instrument instrument, float absoluteTime)
    {
        _score.RecordHit(instrument, absoluteTime);
    }
    
    public static bool DoNotesOverlap(Note a, Note b)
    {
        return (a.hitTime + MusicCore.Instance.trailTime) >= (b.hitTime - MusicCore.Instance.leadTime) &&
               (b.hitTime + MusicCore.Instance.trailTime) >= (a.hitTime - MusicCore.Instance.leadTime);
    }

    public static bool IsHit(Note a, float relativeTime)
    {
        return ((a.hitTime - MusicCore.Instance.leadTime) < relativeTime) &&
            (relativeTime < (a.hitTime + MusicCore.Instance.trailTime));
    }
}