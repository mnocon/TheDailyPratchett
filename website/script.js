	var $content = $("#content");
	var $context = $("#context");
	var $author = $("#author");	
	
	var oneDay = 24*60*60*1000; // hours*minutes*seconds*milliseconds
	var launchDate = new Date(2015, 03, 21, 12);
	var todayDate = new Date();
	
	$(document).ready(function() {	
		$.getJSON( "website/quotes.json", function( data ) {
			quotesArray = data;
			var diffDays = Math.round(Math.abs((todayDate.getTime() - 	launchDate.getTime())/(oneDay)));
			var quoteToSet = diffDays % quotesArray.length;
			setQuote(quoteToSet);
			});
	});
	
	function setQuote(i){
	$content.text(quotesArray[i].Content);
	$author.text(quotesArray[i].Author);
	$context.text(quotesArray[i].Context);
	}