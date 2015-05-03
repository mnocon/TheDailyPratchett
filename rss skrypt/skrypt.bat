RssGenerator "..\website\quotes.json" "The Daily Pratchett" "http://rolieolie.github.io/TheDailyPratchett/" "A quote from Sir Terry Pratchett every day." "..\website\rss.xml" "2015-04-20" "http://rolieolie.github.io/TheDailyPratchett/website/favicon.ico" "http://rolieolie.github.io/TheDailyPratchett/website/rss.xml" 5

pause
git checkout gh-pages
git add ..\website\rss.xml
git commit -m "New rss file. %DATE%"
git push origin gh-pages

