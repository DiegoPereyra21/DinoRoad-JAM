//reconfiguracion COMPLETA de los dinos, antes estaban de forma muy mal pegada. 
public abstract class PersonajeBase
{
    public abstract string Nombre { get; }
    public abstract int Costo { get; }
    public abstract float Salto { get; }//jumpforce
    public abstract float MultiplicadorVelocidad { get; }//velocidad extra del mundo, la idea es q cada dino "sea" mas rapido
}

public class Dino : PersonajeBase
{
    public override string Nombre => "Dino";
    public override int Costo => 0;
    public override float Salto => 15f;
    public override float MultiplicadorVelocidad => 1f;
}
public class Dino1 : PersonajeBase
{
    public override string Nombre => "Dino1";
    public override int Costo => 60;
    public override float Salto => 17f;
    public override float MultiplicadorVelocidad => 1.05f;
}
public class Dino2 : PersonajeBase
{
    public override string Nombre => "Dino2";
    public override int Costo => 90;
    public override float Salto => 20f;
    public override float MultiplicadorVelocidad => 1.1f;
}
public class Dino3 : PersonajeBase
{
    public override string Nombre => "Dino3";
    public override int Costo => 120;
    public override float Salto => 22f;
    public override float MultiplicadorVelocidad => 1.15f;
}