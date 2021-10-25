# Build EOS

require 'rexml/document'

task :default => [ :all ]

progroot = ENV['ProgramFiles']
ver = '12.0'
msbuild = progroot + '\MSBuild\\' + ver + '\Bin\msbuild.exe'
nuget = 'packages\NuGet.CommandLine.2.8.3\tools\NuGet.exe'
nunit = 'packages\NUnit.Runners.2.6.4\tools\nunit-console.exe'

buildno = "0"
if ENV['BUILD_NUMBER'] then
  buildno = ENV['BUILD_NUMBER']
end

ENV['EnableNuGetPackageRestore'] = 'true'

# Find all of the projects, this list will be used to run the StyleCop checks.
projects = []
Dir.glob('**/*.csproj').each do |p|
  projects << p.split('/')[0]
end

task :all => [ :nuget, :solution, :stylecop, :codeanalysis, :test ]

task :incremental => [ :nuget, :solution, :test ]

# make sure the rest of the script runs from the folder where this file is
$topdir = File.dirname(__FILE__)
Dir.chdir($topdir)

task :nuget do
  sh "\"#{nuget}\" restore"
end

task :i18n do
  # This must be done before the build so that the generated files end up in the distribution package.
  sh "EOS2.Web/locale/pull-in-translations.bat"
  sh "package.i18n\\i18n.PostBuild.exe EOS2.Web\\web.config"
  Dir.chdir "EOS2.Web/locale" do
    sh "ruby generate-test-translation-in-af.rb"
    sh "ruby translate-en-US-to-en-GB.rb"
  end
end

task :solution => [:i18n] do
  sh "\"#{msbuild}\" /verbosity:quiet \"\EOS2.Web.sln\" /p:DeployOnBuild=true /p:PublishProfile=\"EOS Zip File\" /p:Configuration=Release"
end

task :stylecop do
  projects.each do |proj|
      Dir.chdir proj do
        sh "\"#{msbuild}\" /verbosity:quiet \"#{proj}.csproj\" /target:ExecuteStyleCop /p:AdditionalLibPaths=\"..\\packages\\MSBuild.Extension.Pack.1.5.0\\tools\\net40\""
      end
  end
end

task :codeanalysis do
  sh "\"#{msbuild}\" /verbosity:quiet \"\EOS2.Web.sln\" /p:Configuration=CodeAnalysis"
end

task :test do
  Dir.mkdir 'results' unless Dir.exist?('results')
  %w(EOS2.Security.Tests EOS2.Services.Tests EOS2.Web.Tests).each do |test|
    sh "\"#{nunit}\" /framework=net-4.5 /xml:results\\#{test.gsub(/\./, '')}Result.xml \"#{test}/bin/Release/#{test}.dll\""
  end
end

task :deploy_test_servers do
  exit_code = nil
  Dir.chdir 'EOS2.Web' do
    # Test system virtual server(s)
    [ 'EOS2TestFirefox1', 'EOS2TestChrome1' ].each do |test_client|
      remote_command = "c:\\Sites\\#{test_client}\\bin\\EOS2Migrate#{test_client}.bat"
      deploy_command = "EOS2.Web.deploy.cmd /Y /M:eos2devwww.corp.com /U:Administrator /P:1t_is_I \"-postSync:runCommand=\"#{remote_command}\",waitInterval=5000,waitAttempts=100\""
      FileUtils.cp "EOS2.Web.SetParameters-#{test_client}.xml", "EOS2.Web.SetParameters.xml"

      puts deploy_command

      # Change the build number, it is called "Version String" in the XML EOS2.Web.SetParameters.xml file.
      doc = REXML::Document.new(IO.read('EOS2.Web.SetParameters.xml'))
      doc.elements.each('//parameters/setParameter[@name=\'Version String\']') do |element|
        element.attributes['value'] = "Build #{buildno}"
      end
      File.open('EOS2.Web.SetParameters.xml', 'w') do |ofile|
        doc.write(ofile, 2)
      end

      IO.popen(deploy_command).each do |line|
        # Look for the exit code in 'Warning: The process 'C:\Windows\system32\cmd.exe' (command line '') exited with code '0x1'.'
        md = /exited with code '(.*)'/.match(line)
        if md
          exit_code = md[1].to_i(16)
        end
        puts "#{line}"
      end
    end
  end
  if exit_code != 0
    raise "Test server deployment failed with exit code of #{exit_code}"
  end
end

task :deploy do
  exit_code = nil
  Dir.chdir 'EOS2.Web' do
    remote_command = "c:\\Sites\\EOS2\\bin\\EOS2Migrate.bat"
    deploy_command = "EOS2.Web.deploy.cmd /Y /M:10.13.224.73 /U:Administrator /P:1t_is_I \"-postSync:runCommand=\"#{remote_command}\",waitInterval=5000,waitAttempts=100\""
    FileUtils.cp "EOS2.Web.SetParameters-dev.xml", "EOS2.Web.SetParameters.xml"
    
    # Change the build number, it is called "Version String" in the XML EOS2.Web.SetParameters.xml file.
    doc = REXML::Document.new(IO.read('EOS2.Web.SetParameters.xml'))
    doc.elements.each('//parameters/setParameter[@name=\'Version String\']') do |element|
      element.attributes['value'] = "Build #{buildno}"
    end
    File.open('EOS2.Web.SetParameters.xml', 'w') do |ofile|
      doc.write(ofile, 2)
    end
    
    IO.popen(deploy_command).each do |line|
      # Look for the exit code in 'Warning: The process 'C:\Windows\system32\cmd.exe' (command line '') exited with code '0x1'.'
      md = /exited with code '(.*)'/.match(line)
      if md
        exit_code = md[1].to_i(16)
      end
      puts "#{line}"
    end
  end
  if exit_code != 0
    raise "Deployment failed with exit code of #{exit_code}"
  end
end
