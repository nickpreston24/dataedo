#!/usr/bin/bash

## https://linuxize.com/post/how-to-use-sed-to-find-and-replace-string-in-files/
find . -type f -name "*.md" -print0 | xargs -0 sed -i 's/<\/?\w+>//g'


