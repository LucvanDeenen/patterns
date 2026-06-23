using FactoryPattern;

IContainer docker = new DockerContainer();
docker.Setup();
docker.Boot();
docker.Display();

Console.WriteLine();

IContainer podman = new PodmanContainer();
podman.Setup();
podman.Boot();
podman.Display();


