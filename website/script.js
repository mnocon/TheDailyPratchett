/* global quotesArray */

$(document).ready(function () {

	var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
	var launchDate = new Date(2015, 4, 1, 12);
	var todayDate = new Date();
	var quotesArray;
	
	$.getJSON("website/quotes.json", function (data) {
	quotesArray = data;
	});
	
	var param = getParameterByName('id');
	var ID;
	if (param === "")
	{
		ID = Math.round(Math.abs((todayDate.getTime() - launchDate.getTime()) / (oneDay))); 
	}
	else
	{
		ID = parseInt(param);
	}
	setQuote(ID, quotesArray);

});

function setQuote(i, quotesArray) {
	if (i < 0) i = 0;
	if (i > quotesArray - 1) i % quotesArray.length;
	
	var $content = $("#content");
	var $context = $("#context");
	var $author = $("#author");
	var $source = $("#source");

	$content.text(quotesArray[i].Content);
	$author.text(quotesArray[i].Author);
	$context.text(quotesArray[i].Context);
	$source.text(quotesArray[i].Source);
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
	
// The world is made up of four elements: Earth, Air, Fire and Water. This is a fact well known even to Corporal Nobbs. It's also wrong. There's a fifth element, and generally it's called Surprise.

// -- (Terry Pratchett, The Truth)