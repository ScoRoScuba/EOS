#!env ruby

usgb = {
    'organization' => 'organisation',
    'ZIP Code' => 'Post Code'
}

File.open('en-GB/translated.po', 'w') do |out|
  s = ""
  File.open('en-GB/messages.po', 'r').each do |line|
    line.chomp!
    if m = /^msgid "(.*)"$/.match(line)
      s = m[1]
      out.puts line
    elsif m = /^msgstr/.match(line)
      changed = false
      usgb.each_pair do |us, gb|
        if m = /(#{us})/i.match(s)
          if m[1][0] =~ /^[A-Z]/
            gb = gb[0].upcase + gb[1..-1]
          end
          s.sub!(/#{us}/i, gb)
          changed = true
          puts "Changed #{us} -> #{gb}"
        end
      end
      if changed
        out.puts "msgstr \"#{s}\""
      else
        out.puts line
      end
    else
      out.puts line
    end
  end
end
ObjectSpace.each_object(File) { |f| f.close if !f.closed? }

File.delete('en-GB/messages.po')
File.rename('en-GB/translated.po', 'en-GB/messages.po')
