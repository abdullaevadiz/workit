$(document).ready(function(){

    /* ask__question form */
    $(".ask__question_btn").on("click", function(event){
        event.preventDefault();
        $(this).parent().animate({left:0},500);
        return false;
    });
    $(".ask__question_close").on("click", function(event){
        event.preventDefault();
        $(this).parent().parent().animate({left:-333},500);
        return false;
    });


    /* block_review */
    $(".btn_close_review").on("click", function(event){
        $(".block_review").slideToggle("fast");
    });


        /* show__block */
    $(".show__block__input_arrow").on("click", function(event){
        $(this).parent().parent().find(".show__block_list").toggleClass("display_none");
    });
    //select event
    $(".show__block_list_item").on("click", function(event){
        var selectedResult = $(this).html();
        var $parent = $(this).parent();
        $parent.addClass("display_none");
        $parent.parent().find(".show_block_input").val(selectedResult);
    });


    /* accordion */
    $(".accordion__item_link").on("click", function(event){
        var $parent =$(this).parent();
        $parent.find(".wrapper__box_katalog").removeClass("display_none");
        $parent.find(".katalog__box").removeClass("display_none");
        $parent.find(".speceality_ask").removeClass("display_none");
    });
    $(".btn_close_katalog").on("click", function(event){
        var $parent = $(this).parent();
        $parent.addClass("display_none");

        var $grand = $parent.parent();
        if ($grand.hasClass("katalog__box"))
            $grand.addClass("display_none");

        $grand.find(".speceality_ask").addClass("display_none");
    });
});


$(document).mouseup(function (e) {
    var container = $(".show__block_list");
    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        $(".show__block_list").addClass("display_none");
    }
});