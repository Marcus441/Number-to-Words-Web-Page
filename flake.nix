{
  inputs = {
    utils.url = "github:numtide/flake-utils";
  };
  outputs = {
    self,
    nixpkgs,
    utils,
  }:
    utils.lib.eachDefaultSystem (
      system: let
        pkgs = nixpkgs.legacyPackages.${system};
      in {
        devShell = pkgs.mkShell {
          buildInputs = with pkgs; [
            dotnet-sdk_9
            nodejs_20
            nodePackages.pnpm
          ];

          shellHook = ''
            echo "Environment ready: .NET 8 (LTS) & Node.js"
          '';
        };
      }
    );
}
