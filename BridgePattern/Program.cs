using BridgePattern;

var tv = new TV();

var remote = new Remote(tv);
remote.TogglePower();
