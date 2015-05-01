/* global quotesArray */
$(document).ready(function () {

	var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
	var launchDate = new Date(2015, 4, 1, 12);
	var todayDate = new Date();

	$.getJSON("website/quotes.json", function (data) {
		quotesArray = data;
		var diffDays = Math.round(Math.abs((todayDate.getTime() - launchDate.getTime()) / (oneDay)));
		var quoteToSet = diffDays % quotesArray.length;
		setQuote(quoteToSet);
	});
});

function setQuote(i) {
	var $content = $("#content");
	var $context = $("#context");
	var $author = $("#author");
	var $source = $("#source");

	$content.text(quotesArray[i].Content);
	$author.text(quotesArray[i].Author);
	$context.text(quotesArray[i].Context);
	$source.text(quotesArray[i].Source);
}
	
// The world is made up of four elements: Earth, Air, Fire and Water. This is a fact well known even to Corporal Nobbs. It's also wrong. There's a fifth element, and generally it's called Surprise.

// -- (Terry Pratchett, The Truth)