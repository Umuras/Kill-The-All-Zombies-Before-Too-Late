using strange.extensions.mediation.impl;

public enum PowerUpsSpawnerEvent
{
    StartSpawningPowerUps,
    DeleteAllPowerUps
}

public class PowerUpsSpawnerMediator : EventMediator
{
    [Inject]
    public PowerUpsSpawnerView view { get; set; }
    [Inject]
    public IPowerUpsSpawnerModel powerUpsSpawnerModel { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(PowerUpsSpawnerEvent.StartSpawningPowerUps, OnStartSpawningPowerUps);
        dispatcher.AddListener(PowerUpsSpawnerEvent.DeleteAllPowerUps, OnDeleteAllPowerUps);
    }
    
    public void OnStartSpawningPowerUps()
    {
        powerUpsSpawnerModel.SpawnPowerUps(view.powerUpsFolder, view.powerUpsSpawnerPoints, view.powerUps);
    }

    public void OnDeleteAllPowerUps()
    {
        powerUpsSpawnerModel.DeleteAllPowerUps(view.powerUpsFolder);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(PowerUpsSpawnerEvent.StartSpawningPowerUps, OnStartSpawningPowerUps);
        dispatcher.RemoveListener(PowerUpsSpawnerEvent.DeleteAllPowerUps, OnDeleteAllPowerUps);
    }
}
