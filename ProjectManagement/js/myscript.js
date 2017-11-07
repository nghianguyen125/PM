
$(document).ready(function () {
    var height_menu = $('ul.menu').height();
    $('.menu_sty').css('height', height_menu);
	$('#news_main_1').css({'opacity': '1', 'display': 'block'}).delay(300);
    slide_1();
    slide_2();
    slideshow();
	showbizActive();
    var width_screen = $(window).width();
    if (width_screen >= 768) {
        $('.right_video').css('height', $('.video_left').height());
        $('.col_right_video').show();
    }
    if (width_screen < 769 && width_screen > 480) {
        set_height_star_box();
    }
    if (width_screen < 769) {
        add_menu_respan();
        box_vide_respon();
    }
    if (width_screen > 480) {
        //box_3_edit();
    }
});

function set_height_star_box() {
    var hei_box = $('.col_2_ss3').height();
//    alert(hei_box);
    $('.col_star_2').css('height', hei_box);
    $('.col_star_1').css('height', hei_box - 10);
}
function slide_1() {
    var owl = $("#owl_demo_1");
    owl.owlCarousel({
        items: 5, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1000px and 901px
        itemsDesktopSmall: [768, 3], // betweem 900px and 601px
        itemsTablet: [480, 2], //2 items between 600 and 0
        itemsMobile: [320, 1] //2 items between 600 and 0
    });
    $(".next").click(function () {
        owl.trigger('owl.next');
    });
    $(".prev").click(function () {
        owl.trigger('owl.prev');
    });
}
function slide_2() {
    var owl = $("#owl_demo_2");
    owl.owlCarousel({
        items: 5, //10 items above 1000px browser width
        itemsDesktop: [1024, 4], //5 items between 1000px and 901px
        itemsDesktopSmall: [768, 3], // betweem 900px and 601px
        itemsTablet: [480, 2], //2 items between 600 and 0
        itemsMobile: [320, 1] //2 items between 600 and 0
    });
    $(".next_stm").click(function () {
        owl.trigger('owl.next');
    });
    $(".prev_stm").click(function () {
        owl.trigger('owl.prev');
    });
}
$(window).resize(function () {
    var width_screen = $(window).width();
    if (width_screen > 768) {
        set_height_star_box();
        $('.right_video').css('height', $('.video_left').height());
        $('.col_right_video').show();
        $('.hide_ul').hide();
    }
    if (width_screen <= 768) {
        $('.hide_ul').show();
        box_vide_respon();
    }
    if (width_screen > 1366) {
        //box_3_edit();
    } else if (width_screen === 1366 || width_screen > 1024) {
        //box_3_edit();
        //num = 4;
    } else if (width_screen === 1024 || width_screen > 768) {
        //box_3_edit();
        //num = 3;
    } else if (width_screen === 768 || width_screen > 480) {
        //box_3_edit();
        //num = 2;
    } else if (width_screen === 480 || width_screen > 320) {
        //num = 1;
        box_vide_respon();
    }
    add_menu_respan();
});

$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.go_top').fadeIn();
    } else {
        $('.go_top').fadeOut();
    }
});
//Click event to scroll to top
$('.go_top').click(function () {
    $('html, body').animate({scrollTop: 0}, 800);
    return false;
});
function showbizActive() {
	$('.col_2_ss3 .center_col').first().addClass('active');
    $('.col_2_ss3 .left_col').first().addClass('active');
	$('.col_2_ss3 .right_col').first().addClass('active');
}
function slideshow() {
    setTimeout(function () {
        action_slide('news_main_1');
        setTimeout(function () {
            action_slide('news_main_2');
            setTimeout(function () {
                action_slide('news_main_3');
                slideshow();
            }, 5000);
        }, 5000);
    }, 5000);
}
function action_slide(id) {
    $('.box_slide').removeClass('active');
    $('.box_slide').css({'opacity': '0', 'display': 'none'}).delay(300);
    $('#' + id).toggleClass('active');
    $('#' + id).css({'opacity': '1', 'display': 'block'}).delay(300);
    ;
}
function show_stock(num) {
    $('.show_stock').removeClass('show');
    $('.num_stock_' + num).addClass('show');
}

function show_show_biz(id) {
    $('.right_col').removeClass('active');
    $('.center_col').removeClass('active');
    $('.left_col').removeClass('active');
    $('.col_centen_' + id).addClass('active');
    $('.show_' + id).addClass('active');
    $('.col_left_' + id).addClass('active');
}
$('.btn_control_col_left i.showicon').click(function () {
    $('.menu_hide').css('left', '0px');
    $('i.hideicon').css('display', 'block');
    $('.ovelay_menu').show();
    $(this).hide();
    var sum_pa = $('.block_scoll_menu').height();
    $('.sum_pa').addClass('hiden');
    $('.sum_pa').css('height', sum_pa);
});
$('.btn_control_col_left i.hideicon').click(function () {
    $('.menu_hide').css('left', '-225px');
    $('.ovelay_menu').hide();
    $('.sum_pa').removeClass('hiden');
    $('.sum_pa').css('height', 'auto');
    $('i.showicon').css('display', 'block');
    $(this).hide();

});

function add_menu_respan() {
    var menu = $('.box_menu').html();
    $('.block_scoll_menu').html(menu);
}
$('.ovelay_menu').click(function () {
    $('.menu_hide').css('left', '-225px');
    $('i.showicon').css('display', 'block');
    $('i.hideicon').css('display', 'none');
    $('.ovelay_menu').hide();
    $('.sum_pa').css('height', 'auto');
    $(this).hide();
});

function box_vide_respon() {
    $temp_video = $('.col_right_video').html();
    $('.hide_ul').html($temp_video);
    $('.col_right_video').hide();
}