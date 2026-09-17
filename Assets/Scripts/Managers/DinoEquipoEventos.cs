using System;
//PATRON OBSERVER, avisa a quien este suscrito al cambiar de dino, asi nadie tiene q conocer los demas botones y tal
public static class DinoEquipoEventos
{
    public static event Action<int> OnDinoEquipado;

    public static void Notificar(int dinoIndexEquipado)
    {
        OnDinoEquipado?.Invoke(dinoIndexEquipado);
    }
}