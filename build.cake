#addin nuget:?package=Cake.Docker

using System.ComponentModel;
string          target      = Argument("target", "Default");
FilePath        ngPath      = Context.Tools.Resolve("ng.cmd");
FilePath        npmPath     = Context.Tools.Resolve("npm.cmd");
DirectoryPath   outputPath  = MakeAbsolute(Directory("./dist"));

Action<FilePath, ProcessArgumentBuilder> Cmd => (path, args) => {
    var result = StartProcess(
        path,
        new ProcessSettings {
            Arguments = args
        });

    if(0 != result)
    {
        throw new Exception($"Failed to execute tool {path.GetFilename()} ({result})");
    }
};

Task("Install-AngularCLI")
    .Does(() => {
    if (ngPath != null && FileExists(ngPath))
    {
        Information("Found Angular CLI at {0}.", ngPath);
        return;
    }

    DirectoryPath ngDirectoryPath = MakeAbsolute(Directory("./Tools/ng"));

    EnsureDirectoryExists(ngDirectoryPath);

    Cmd(npmPath,
        new ProcessArgumentBuilder()
                .Append("install")
                .Append("--prefix")
                .AppendQuoted(ngDirectoryPath.FullPath)
                .Append("@angular/cli")
    );
    ngPath = Context.Tools.Resolve("ng.cmd");
});

Task("Clean")
    .Does( ()=> {
        CleanDirectory(outputPath);
});

Task("Install")
    .IsDependentOn("Clean")
    .Does( ()=> {
    Cmd(npmPath,
        new ProcessArgumentBuilder()
            .Append("install")
    );
});

Task("Build")
    .IsDependentOn("Install-AngularCLI")
    .IsDependentOn("Install")
    .Does( ()=> {
    Cmd(ngPath,
        new ProcessArgumentBuilder()
            .Append("build")
            .Append("--output-path")
            .AppendQuoted(outputPath.FullPath)
    );
});

Task("Docker-Build")
  .Does(() => {
      var settings = new DockerImageBuildSettings { Tag = new[] {"registry/gitlab.com/lowlandtech/app:latest" }};
      DockerBuild(settings, "./");
  });

Task("Docker-Push")
  .IsDependentOn("Docker-Build")
  .Does(() => {
      var settings = new DockerImagePushSettings();
      DockerPush(settings, "registry/gitlab.com/lowlandtech/app:latest");
  });

Task("Default")
    .IsDependentOn("Build");
    // .IsDependentOn("Docker-Push");

RunTarget(target);
