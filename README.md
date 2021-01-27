# Installation

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/92c55316441c4cac929a0cb8018af70a)](https://app.codacy.com/gh/WhiteOlivierus/PlayTestToolkitPackage?utm_source=github.com&utm_medium=referral&utm_content=WhiteOlivierus/PlayTestToolkitPackage&utm_campaign=Badge_Grade_Settings)

The first 3 steps in this process are a little involved. So this should be done by a developer.
Or if you host your own server you should be able to do this.

## Setup server

[Video tutorial](https://www.youtube.com/watch?v=LhiNf0C7AEU)

### Requirements

* [Git](https://github.com/git-guides/install-git)
* [Docker](https://docs.docker.com/engine/install/ubuntu/#install-from-a-package)
* [Docker-compose](https://docs.docker.com/compose/install/#install-compose)
* As server with a minimum of 1.5 GB of RAM (You can use a hosting website like [Google Cloud](https://cloud.google.com/))

### Step 1

Make sure you have `Git` installed.
Connect to your server through ssh or any other means.
Now use the command given down here to clone the repository.

```Shell Session
git clone https://github.com/WhiteOlivierus/PlayTestToolkitPackage.git
```

### Step 2

When the repository has been cloned you can navigate to the folder with the `Docker` compose.

```Shell Session
cd PlayTestToolkit/PlayTestToolkitWeb
```

### Step 3

Make sure that you have `Docker` and `Docker-Compose` installed.
Now you can run the `Docker-Compose` command with -d for detached.

```Shell Session
sudo docker-compose up -d
```

**Note:** it won't give 5 times a green done you should run this command without -d to report a bug.

### Step 4

If you haven't all ready, you should open the port 80 in your firewall and on your route so you can access this server.
This is dependent on what server and router you have, so you can check this with your web provider and server provider.

### Step 5

Now we can check if the setup was success full.
By default the `Nginx server` that servers up the API and web interface is configured to pass all traffic through localhost.
So if you go too your browser and enter the `server IP`, you should see the play test toolkit web interface.

## Advanced server setup

### Reverse proxy

If you have a domain that you want to use for this, you can configure the `nginx.config` for yourself.
The config file is located here relative to the git repository root.

```Shell Session
cd PlayTestToolkit/PlayTestTookitWeb/Nginx/nginx.config
```

## Setup Unity

[Video tutorial](https://www.youtube.com/watch?v=d6hDHxiAikA)

### Step 1

Go to your unity project folder and find the folder called `Packages`.
In this folder you will find a file called `manifest.json`.
When you open this file you will see a list of dependencies.

Here you will add after the starting `{` this code snippet.

``` JSON
"scopedRegistries": [
    {
        "name": "Play Test Toolkit",
        "url": "https://unitypackages.dutchskull.com/",
        "scopes": [
            "com.dutchskull"
        ]
    }
],
```

Or add this entry too the existing scoped registries if you have more of them.

Now save this file and let's got Unity.

### Step 2

In Unity we will have to install the `Play Test Toolkit package`.
This can be done by going to `Window/Package Manager`.
When the package manager is opened you can look in the left corner of the window to find a drop down with the text `Unity Registry`.
In this list that appears when you click on it, there should be a entry that says `My Registries` and you can select that.

**Note:** If you don't see `My Registries` you need to go back to step one of [Setup Unity](#Setup-Unity).

Here you will find the `Play Test Toolkit` package, and like any other package you can select it and install the package.
This package also depends on the samples to be imported.
In the `Package Manager` under `Play Test Toolkit` there should be a text with samples, and under there a text that says `Resources`. 
Now import the resources by pressing the `Import in to project` button.

### Step 3

As a last step we need to connect the `Play Test Toolkit` to the server.
This can be done by going to `Edit/Project Settings`.
Here there will be a entry that says `Play Test Toolkit`.
When you go to that tab it will show a empty field for a web url.
This is where you will add the `server IP` or `sub domain` to the web side of `Play Test Toolkit`.

Now we are all done to start setting up play tests.

# Usage

[Video tutorial](https://www.youtube.com/watch?v=RbgRParF7BM)

## Setting up play test
### Step 1
In the Play Test Toolkit window in unity you can press the `Setup a play test` button. Here you can fill in the information needed to make a play test.
When you don't want to build directly after this, you can just press `save` and edit it later again.
If you would like to make a build press the `save and build` button. This will build your play test and upload it to the server.
## Sharing play test
In the Play Test Toolkit window in unity you can press the `share` button. This will copy a link too the shareable build.

## Seeing collected data
Go to the web interface using your `server IP`. Then you can navigate too the play test which you want to look at it's data.
