jQuery(function () {
	"use strict";

    /*global jQuery, $, google, slides */


            // site preloader -- also uncomment the div in the header and the css style for #preloader
				var preloader = document.getElementById('preloader');
				window.onload = function() {
					setTimeout(removePreloader , 500);
				};

				function removePreloader() {
					document.body.removeChild(preloader);
				};

              jQuery(document).ready(function($) {


        jQuery('.navbar-collapse').click('li', function() {
            jQuery('.navbar-collapse').collapse('hide');
        });


       $('.left_bar').onePageNav();


        $('a.btn-reg').click(function() {
            $('html, body').animate({ scrollTop:$('#pricing_table').offset().top - 0 }, 1000);
            return false;
        });

        $('a.btn-loc').click(function() {
            $('html, body').animate({ scrollTop:$('#location').offset().top - 0 }, 1000);
            return false;
        });

        $('a.btn-spek').click(function() {
            $('html, body').animate({ scrollTop:$('#speakers').offset().top - 0 }, 1000);
            return false;
        });

        // We only want these styles applied when javascript is enabled
        $('div.navigation').css({ 'float' : 'left'});
        $('div.content').css('display', 'block');

        // Initially set opacity on thumbs and add
        // additional styling for hover effect on thumbs
        var onMouseOutOpacity = 0.67;
        $('#thumbs ul.thumbs li').opacityrollover({
            mouseOutOpacity:   onMouseOutOpacity,
            mouseOverOpacity:  1.0,
            fadeSpeed:         'fast',
            exemptionSelector: '.selected'
        });

        // Initialize Advanced Galleriffic Gallery
        $('#thumbs').galleriffic({
            delay:                     2500,
            numThumbs:                 9,
            preloadAhead:              10,
            enableTopPager:            true,
            enableBottomPager:         true,
            maxPagesToShow:            7,
            imageContainerSel:         '#slideshow',
            controlsContainerSel:      '#controls',
            captionContainerSel:       '#caption',
            loadingContainerSel:       '#loading',
            renderSSControls:          true,
            renderNavControls:         true,
            playLinkText:              'Play Slideshow',
            pauseLinkText:             'Pause Slideshow',
            prevLinkText:              '&lsaquo; Previous Photo',
            nextLinkText:              'Next Photo &rsaquo;',
            nextPageLinkText:          'Next &rsaquo;',
            prevPageLinkText:          '&lsaquo; Prev',
            enableHistory:             false,
            autoStart:                 false,
            syncTransitions:           true,
            defaultTransitionDuration: 3000,
            onSlideChange:             function(prevIndex, nextIndex) {
                // 'this' refers to the gallery, which is an extension of $('#thumbs')
                this.find('ul.thumbs').children()
                    .eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
                    .eq(nextIndex).fadeTo('fast', 1.0);
            },
            onPageTransitionOut:       function(callback) {
                this.fadeTo('fast', 0.0, callback);
            },
            onPageTransitionIn:        function() {
                this.fadeTo('fast', 1.0);
            }
        });

        $('#pricing_table').parallax("50%", 0.1);
        $('#be_partners').parallax("50%", 0.1);
        $('#newsleter').parallax("50%", 0.1);
        $('#per_action').parallax("50%", 0.1);
        $('#spek_action').parallax("50%", 0.1);
        $('#contact_action').parallax("50%", 0.1);


    });

            var myOptions = {
				zoom: 16,
				scrollwheel: false,
				center: new google.maps.LatLng(53.385873, -1.471471),
				mapTypeId: google.maps.MapTypeId.ROADMAP,
				styles: [
							{
								"featureType": "water",
								"stylers": [
									{
										"visibility": "on"
									},
									{
										"color": "#acbcc9"
									}
								]
							},
							{
								"featureType": "landscape",
								"stylers": [
									{
										"color": "#f2e5d4"
									}
								]
							},
							{
								"featureType": "road.highway",
								"elementType": "geometry",
								"stylers": [
									{
										"color": "#c5c6c6"
									}
								]
							},
							{
								"featureType": "road.arterial",
								"elementType": "geometry",
								"stylers": [
									{
										"color": "#e4d7c6"
									}
								]
							},
							{
								"featureType": "road.local",
								"elementType": "geometry",
								"stylers": [
									{
										"color": "#fbfaf7"
									}
								]
							},
							{
								"featureType": "poi.park",
								"elementType": "geometry",
								"stylers": [
									{
										"color": "#c5dac6"
									}
								]
							},
							{
								"featureType": "administrative",
								"stylers": [
									{
										"visibility": "on"
									},
									{
										"lightness": 33
									}
								]
							},
							{
								"featureType": "road"
							},
							{
								"featureType": "poi.park",
								"elementType": "labels",
								"stylers": [
									{
										"visibility": "on"
									},
									{
										"lightness": 20
									}
								]
							},
							{},
							{
								"featureType": "road",
								"stylers": [
									{
										"lightness": 20
									}
								]
							}
						]
			};

			var map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);


			$(function() {
				var endDate = "September 10, 2017 15:03:25";

				$('.countdown.styled').countdown({
				  date: endDate,
				  render: function(data) {
					$(this.el).html("<div><div><div>" + "<h2>" + this.leadingZeros(data.days, 3) + "</h2>" + " <span>Days</span></div></div></div><div><div><div>" + "<h2>" + this.leadingZeros(data.hours, 2) + "</h2>" + " <span>Hours</span></div></div></div><div><div><div>" + "<h2>" + this.leadingZeros(data.min, 2) + "</h2>" + " <span>Minuts</span></div></div></div><div><div><div>" + "<h2>" + this.leadingZeros(data.sec, 2) + "</h2>" + " <span>Seconds</span></div></div></div>");
				  }
				});
			  });


			$('#slides').superslides({
			  animation: 'fade',
			  play: 2000,
			  inherit_width_from: slides,
			  pagination: false,
			});


}());