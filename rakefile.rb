# this project may be viewed here: https://lou-parslow.github.io/HelloAD/
VERSION="0.0.0"
require "raykit"
SITE_DIR="artifacts/HelloAD.#{VERSION}"

task :build do
 FileUtils.cp("README.md","src/HelloAD/wwwroot/")
  run "dotnet publish src/HelloAD/HelloAD.csproj -c Release -o dist"
  FileUtils.mkdir("artifacts") unless Dir.exist? "artifacts"
  FileUtils.mkdir(SITE_DIR) unless Dir.exist? SITE_DIR
  puts "  copying files to #{SITE_DIR}"
  FileUtils.cp_r "dist/wwwroot/.", SITE_DIR
  # for github pages, a .nojekyll file is required to prevent github from ignoring files starting with an underscore
  File.open("#{SITE_DIR}/.nojekyll", "w") { }
  Raykit::Text::replace_in_file("#{SITE_DIR}/index.html", '<base href="/" />', '<base href="/Hello.Blazor.Wasm/" />')
end

task :update_docs => [:build] do
  mkdir "docs" unless Dir.exist? "docs"
  FileUtils.cp_r "#{SITE_DIR}/.", "docs"
end

task :default => [:build,:update_docs, :integrate, :push] do
end
