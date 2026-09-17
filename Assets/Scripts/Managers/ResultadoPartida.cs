//recreado para esos nombres exactos, no cambiarlos
public abstract class ResultadoPartida
{
    public abstract string NombreEscena { get; }
}

public class Victoria : ResultadoPartida
{
    public override string NombreEscena => "WinScreen";
}

public class Derrota : ResultadoPartida
{
    public override string NombreEscena => "LoserScreen";
}