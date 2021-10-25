#!env ruby

File.open('af/translated.po', 'w') do |out|
  s = ""
  File.open('af/messages.po', 'r').each do |line|
    line.chomp!
    if m = /^msgid "(.*)"$/.match(line)
      s = m[1]
      out.puts line
    elsif m = /^msgstr/.match(line)
      out.puts "msgstr \"|#{s}|\""
    else
      out.puts line
    end
  end
end
ObjectSpace.each_object(File) { |f| f.close if !f.closed? }

File.delete('af/messages.po')
File.rename('af/translated.po', 'af/messages.po')
