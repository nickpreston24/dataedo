## Writing a dockerfile that adds features to an existing image

[

![Ted Spence](https://miro.medium.com/v2/resize:fill:88:88/0*jBizWW8qVTeqjFEI.jpeg)



](https://ted-spence.medium.com/?source=post_page-----a1b7c5fc308c--------------------------------)[

![tedspence.com](https://miro.medium.com/v2/resize:fill:48:48/1*oynTmr85CbU2euXlrGH_Tg.jpeg)



](https://tedspence.com/?source=post_page-----a1b7c5fc308c--------------------------------)

I wrote [containerized SQL Server on Docker](https://tedspence.com/containerized-sql-server-on-docker-ff6082464be7) a few months ago when I migrated my company from a “developer desktop setup tutorial”-style environment to a “docker compose up”.

Using SQL Server on Docker, we were able to end manual installations of SQL Server which wasted developer time and made it easy to commit a mistake that would prevent the rest of our setup scripts from succeeding. Perhaps someday Microsoft’s SQL Server team will phase out complex UI-based installers; but by then we’ll all have [migrated to Postgres](https://tedspence.com/part-1-designing-an-api-with-dotnet-and-postgres-4fbefb898e68).

Now that we’ve committed to Docker, we have decided to invest time in new techniques to make more use of containers. In today’s short blog post I will walk us through installing full-text search on SQL Server in docker using a Dockerfile.

![](https://miro.medium.com/v2/resize:fit:640/1*7Nl-705mb4HoK7zmaldSvg.jpeg)

Photo by [Micah Boswell](https://unsplash.com/@micahboswell?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText) on [Unsplash](https://unsplash.com/photos/00nHr1Lpq6w?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText)

## What’s the latest version of SQL Server on Docker?

If you visit the [Docker Catalog and search for sqlserver](https://hub.docker.com/search?q=sqlserver), you’ll find a lot of images — and, as of the writing of this article, none of them are published by Microsoft.

On the other hand, Microsoft does publish an official reference image of sqlserver, but you have to do some searching for it. [Microsoft’s official Docker tutorial](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&pivots=cs1-bash) says that they have an instance of SQL Server 2022 available:

```
<span id="1756" data-selectable-paragraph="">sudo docker pull mcr.microsoft.com/mssql/server:2022-latest</span>
```

If you keep clicking through documentation, you can find this [Microsoft SQL Server Docker hub page](https://hub.docker.com/_/microsoft-mssql-server) which lists a bunch of different versions — but none of them include full-text search. So how do you install that?

Our original solution was to just manually install full-text search using the Docker Desktop terminal. That’s nice, but let’s go further and create a proper build script.

## Using a Dockerfile to set up SQL Server with full-text search

A [Dockerfile](https://docs.docker.com/engine/reference/builder/) is a script that explains to Docker how to start with a known state and then transform it into another state. If you use your Dockerfile correctly, you can benefit from caching: each intermediate transformation can be cached to save you the time when you need to rebuild it again.

If you just want to fast-forward to the end, take a look at my resulting [Dockerfile for SQL Server with full-text search](https://github.com/tspence/docker-examples/blob/main/sqlserver-fulltext/Dockerfile). Otherwise, let’s walk through how we get there.

## Start with a known image using FROM

The first step in a Dockerfile is to define our starting point. We’ll do that by using Microsoft’s official 2022 SQL Server image as a **FROM** statement:

```
<span id="2ac6" data-selectable-paragraph=""><br>FROM mcr.microsoft.com/mssql/server:2022-latest</span>
```

## Install CURL as user ‘root’

The next step in our script is to switch to a user with permission to run apt-get. If you don’t use this step, you won’t be able to run any installations.

```
<span id="b44e" data-selectable-paragraph=""><br>USER root</span>
```

We will now install CURL, a fantastic program which lets us retrieve files from remote Internet servers. Since Microsoft publishes a package for SQL Server full-text search for Ubuntu, this is what we’ll need to do to install it.

```
<span id="8e06" data-selectable-paragraph=""><br>RUN apt-get update<br>RUN apt-get install -yq gnupg gnupg2 gnupg1 curl apt-transport-https</span>
```

## Add full-text search via apt-get

Although Microsoft does publish packages for Ubuntu, you can’t install them by default. Instead, you have to tell Ubuntu that it’s allowed to use Microsoft package sources first.

You do that by fetching a Microsoft package key and adding it via `apt-key` . Once this is done, you can then tell Ubuntu to use the Microsoft package source as one place to find new packages to install.

After both these steps are complete, you can then do the real action — you can `apt-get install -y mssql-server-fts`.

```
<span id="1ef8" data-selectable-paragraph=""><br>RUN curl https://packages.microsoft.com/keys/microsoft.asc -o /var/opt/mssql/ms-key.cer<br>RUN apt-key add /var/opt/mssql/ms-key.cer<br>RUN curl https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list -o /etc/apt/sources.list.d/mssql-server-2022.list<br>RUN apt-get update<br><br><br>RUN apt-get install -y mssql-server-fts</span>
```

## Clean up and set an entrypoint

The final step in a Dockerfile is to do whatever cleanup is necessary — in our case, we want to remove any unnecessary apt-get leftovers remain from our installation work. Finally, we’ll set an entrypoint to make sure someone can use this SQL Server.

```
<span id="45de" data-selectable-paragraph=""><br>RUN apt-get clean<br>RUN <span>rm</span> -rf /var/lib/apt/lists<br><br><br>ENTRYPOINT [ <span>"/opt/mssql/bin/sqlservr"</span> ]</span>
```

## Creating a docker compose script for this file

To begin using this script you should save this file to sqlserver-fulltext.Dockerfile and write a docker-compose.yaml file that references it. You’ll need to specify two environment variables: `SA_PASSWORD` and `ACCEPT_EULA`.

Here’s what a working docker compose YAML file looks like:

```
<span id="cb0f" data-selectable-paragraph=""><span>version:</span> <span>"3.2"</span><br><span>services:</span><br><br>  <span>sqlserver:</span><br>    <span>container_name:</span> <span>sqlserver</span><br>    <span>build:</span><br>      <span>dockerfile:</span> <span>sqlserver-fulltext.Dockerfile</span><br>    <span>ports:</span><br>      <span>-</span> <span>"1433:1433"</span><br>    <span>environment:</span><br>      <span>SA_PASSWORD:</span> <span>"Some4Complex#Password"</span><br>      <span>ACCEPT_EULA:</span> <span>"Y"</span></span>
```

If you’d prefer, you can just download this directly from [my Github repository](https://github.com/tspence/docker-examples).