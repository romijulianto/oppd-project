ew.events={};ew.charts={};ew.clientScript=function(){ew.selectOptions.minimumResultsForSearch=3;const $tableCaptionHeaderDiv=$(".content-header > .container-fluid > .row > .col-sm-6").first();const $breadCrumbHeaderDiv=$(".content-header > .container-fluid > .row > .col-sm-6:eq(1)");$breadCrumbHeaderDiv.removeClass("col-sm-6").addClass("col-sm-12");$tableCaptionHeaderDiv.remove();const $breadCrumbOl=$(".breadcrumb").first();$breadCrumbOl.removeClass("float-sm-end").addClass("float-sm-start");$("#ew-navbar").after((function(){return'<div id="logo" hidden>'+'<img src="/images/logo.png" alt="" class="brand-image ew-brand-image" style="width:129,2px;height:33px;">'+"</div>"}));$("#logo").after('<div id="title" style="text-align:center; width: 100%;"><h4 style="font-weight: bold;">Override Price PS Digitalization</h4></div>');$(".user-panel").remove();$("#title").after('<div id="username" style="text-align:right; margin-top:5px;"><h6>'+$("#ew-navbar-end").children(".nav-item").children(".dropdown-menu").children(".dropdown-header").text()+"</h6></div>");function checkBody(){if($("body").hasClass("sidebar-collapse")){$("#logo").attr("hidden",true)}else{$("#logo").attr("hidden",false)}}$("#ew-navbar").click((function(){checkBody()}))};ew.startupScript=function(){$("#tab nav a").click((function(){const id=$(this).data("id");if(!$(this).hasClass("active")){$("#tab nav a").removeClass("active");$(this).addClass("active");$(".tab-content").hide();$(`[data-content=${id}]`).fadeIn()}}))};