/*! jQuery v1.7.2 jquery.com | jquery.org/license */
	
/******** ad scroll http://www.wduffy.co.uk/jScroll  *********/
						   
(function($){
$.fn.jScroll=function(options){
var opts=$.extend({},$.fn.jScroll.defaults,options); if ($('.d_listnews_bottom').offset()===null) {return;}
var maxmagin=$('.d_listnews_bottom').offset().top-$(this).outerHeight();
return this.each(function(){
var $element=$(this);
var $window=$(window);
var locator=new location($element);
$window.scroll(function(){
	if($window.width()>768){		
		$element.stop().animate(locator.getMargin($window),opts.speed);
	}
	else{
		$element.css("margin-top","0");
	}
});
});
function location($element)
{
this.min=$element.offset().top;
this.originalMargin=parseInt($element.css("margin-top"),2)||0;
this.getMargin=function($window)
{
var max=$element.parent().height()-$element.outerHeight();
var margin=this.originalMargin;
if($window.scrollTop()>=this.min){margin=margin+opts.top+$window.scrollTop()-this.min;}
if(margin>max){margin=max;}
if(margin>maxmagin-this.min){margin=maxmagin-this.min;}
	return({"margin-top":margin+'px'});
}
}
};
$.fn.jScroll.defaults={
speed:"slow",
top:2
};
})(jQuery);
$(function(){$(".adscroll").jScroll();});

