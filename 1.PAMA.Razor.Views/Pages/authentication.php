<?php 
// echo ROOT_URL;

// die(); 
?>
<!DOCTYPE html>
<html lang="en">
<head>
	<title><?= HEAD_NAME?> - Authentication</title>
	<?php $this->load->view("_partials/head_css.php");?>
	<style>
				
				
	.linePreloader{
	    width:100%;
	    height:2px;
	    background:linear-gradient(to right,green,green);
	    background-color:#ccc;
	    position:absolute;
	    top:0;
	    bottom:0;
	    left:0;
	    right:0;
	    /*margin:auto;*/
	    border-radius:4px;
	    background-size:20%;
	    background-repeat:repeat-y;
	    background-position:-25% 0;
	    animation:scroll 1.2s ease-in-out infinite;
	  }
	  
	 @keyframes scroll{
	    50%{background-size:80%}
	    100%{background-position:125% 0;}
	 }
	</style>
</head>
<body>
	<?php if ($background != null || $background != ""): ?>
		
	<body class="login-page" style="background:url('<?= $background?>') center no-repeat;background-size: cover;">
	<?php else: ?>
	<body class="login-page" >
	<?php endif ?>
    <div class="login-box">
        <div class="logo">
            <a href="javascript:void(0);"><font class="base_color1">Meeting</font><b class="base_color2">Portal</b></a>
            <small><?= APP_NAME?> v<?=APP_VERSION?></small>
        </div>
        <div class="card">
            <div class="body">
            	<div id="id_loader"></div>
            	
                <form id="sign_in" method="POST" >
                    <div class="msg">Sign in to start your session</div>
                    <!-- <div class=""></div> -->
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">person</i>
                        </span>
                        <div class="form-line">
                            <input type="text" class="form-control" name="username" placeholder="Username " required autofocus>
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">lock</i>
                        </span>
                        <div class="form-line">
                            <input type="password" class="form-control" name="password" placeholder="Password" required>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 p-t-5">
                        </div>
                        <div class="col-xs-4">
                        	<button type="submit" class="btn btn-success btn-circle-lg waves-effect waves-circle waves-float">
                                <i class="material-icons">keyboard_arrow_right</i>
                            </button>
                        </div>
                    </div>

                    <div class="row m-t-15 m-b--20">
                        <div class="col-xs-12">
                        	<a style="text-decoration: underline;" href="https://drive.google.com/drive/folders/1sZeLEzlOyjK4bkNQ0eKUYz2kCPCu9TJA?usp=sharing" target="__blank">Click Here to Read Documentation & Manual Guide</a>
                        </div>
                        <!-- <div class="col-xs-6 align-right"> -->
                        	<!--  -->
                        <!-- </div> -->
                    </div>
                </form>
            </div>
        </div>
    </div>
	<div>
	    <!-- Jquery Core Js -->
    <script src="<?php echo base_url("assets/theme/plugins/jquery/jquery.min.js");?>"></script> 
    <!-- Bootstrap Core Js -->
    <script src="<?php echo base_url("assets/theme/plugins/bootstrap/js/bootstrap.js");?>"></script>
    <!-- Slimscroll Plugin Js -->
    <script src="<?php echo base_url("assets/theme/plugins/jquery-slimscroll/jquery.slimscroll.js");?>"></script>
    <!-- Waves Effect Plugin Js -->
    <script src="<?php echo base_url("assets/theme/plugins/node-waves/waves.js");?>"></script>
    <!-- Jquery CountTo Plugin Js -->
    <script src="<?php echo base_url("assets/theme/plugins/jquery-countto/jquery.countTo.js");?>" ></script>
    <!-- Custom Js -->
    <script src="<?php echo base_url("assets/theme/js/admin.js");?>" ></script>
    <!-- <script src="<?php echo base_url("assets/theme/js/pages/index.js");?>" ></script> -->
    <!-- Demo Js -->
    <script src="<?php echo base_url("assets/theme/js/demo.js");?>" ></script>
    <!-- Select Plugin Js -->
    <script src="<?php echo base_url("assets/theme/plugins/bootstrap-select/js/bootstrap-select.js");?>"></script>
    <script src="<?php echo base_url("assets/theme/plugins/bootstrap-notify/bootstrap-notify.js");?>"></script>
    <script>
        // $(function () {
        //     $.AdminBSB.browser.activate();
        //     $.AdminBSB.leftSideBar.activate();
        //     $.AdminBSB.rightSideBar.activate();
        //     $.AdminBSB.navbar.activate();
        //     $.AdminBSB.dropdownMenu.activate();
        //     $.AdminBSB.input.activate();
        //     $.AdminBSB.select.activate();
        //     $.AdminBSB.search.activate();

        //     setTimeout(function () { $('.page-loader-wrapper').fadeOut(); }, 50);
        // });
        // 
        function showNotification(colorName, text, placementFrom, placementAlign, animateEnter, animateExit) {
            if (colorName === null || colorName === '') { colorName = 'bg-black'; }
            if (text === null || text === '') { text = 'Turning standard Bootstrap alerts'; }
            if (animateEnter === null || animateEnter === '') { animateEnter = 'animated fadeInDown'; }
            if (animateExit === null || animateExit === '') { animateExit = 'animated fadeOutUp'; }
            var allowDismiss = true;

            $.notify({
                message: text
            },
                {
                    z_index: 9999,
                    type: colorName,
                    allow_dismiss: allowDismiss,
                    newest_on_top: true,
                    timer: 1000,
                    placement: {
                        from: placementFrom,
                        align: placementAlign
                    },
                    animate: {
                        enter: animateEnter,
                        exit: animateExit
                    },
                    template: '<div data-notify="container" class="bootstrap-notify-container alert alert-dismissible {0} ' + (allowDismiss ? "p-r-35" : "") + '" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
                });
        }
    </script>
    <script src='<?php echo base_url("assets/theme/plugins/jquery-validation/jquery.validate.js");?>'></script>
	</div>
	<script>
		$(function () {
		    $('#sign_in').validate({
		        highlight: function (input) {
		            // console.log(input);
		            $(input).parents('.form-line').addClass('error');
		        },
		        unhighlight: function (input) {
		            $(input).parents('.form-line').removeClass('error');
		        },
		        errorPlacement: function (error, element) {
		            $(element).parents('.input-group').append(error);
		        },
		        submitHandler: function(form) {
				    // $('#sign_in')[0].submit();
				    // console.log(form)
				    // 
				}

		    });
		    // console.log(d);	
		});
		$('#sign_in').submit(function(e){
			e.preventDefault();
			if($(this).valid() == false){
				return false;
			}
			$.ajax({
				url : "authentication/login",
				type : "POST",
				dataType : "json",
				data: $(this).serialize(),	
				beforeSend: function(XMLHttpRequest){
					$('#id_loader').html('<div class="linePreloader"></div>');
				},
				success: function(data){
					// console.log(data)
					
					if(data.status == "fail"){
						$('#id_loader').html('');
						showNotification('alert-danger', data.msg,'bottom','left')
					}else if(data.status == "success"){
						showNotification('alert-success', "Success, please wait for redirect.",'bottom','left')
						setTimeout(function(){
							$('#id_loader').html('');
							window.location.href = "./";
						}, 1000);
					}
					
				},
				error: function (xhr, ajaxOptions, thrownError) {
					$('#id_loader').html('');
			        // console.log("Error :> ", xhr.status);
			        if(ajaxOptions == "parsererror"){
			        	var msg = "Status Code 500, Error Server bad parsing";
			        	showNotification('alert-danger', msg,'bottom','left')
			        }else{
			        	var msg ="Status Code "+ xhr.status + " Please check your connection !!!";
			        	showNotification('alert-danger', msg,'bottom','left')
			        }
			        // console.log( ajaxOptions);
			    },
				xhr: function()
			  	{
				    var xhr = $.ajaxSettings.xhr();
			        xhr.onprogress = function e() {
			            // For downloads
			            if (e.lengthComputable) {
			                console.log(e.loaded / e.total, " progress");
			            }
			        };
			        xhr.upload.onprogress = function (e) {
			            // For uploads
			            if (e.lengthComputable) {
			                console.log(e.loaded / e.total, " progress upload");
			            }
			        };
			        return xhr;
			  	},
			})
			// handleFormSubmit($('#sign_in'));
		});
		// function handleFormSubmit(form, input) {
	 //        // validate the form against the constraints
	 //        var errors = validate(form, constraints);
	 //        // then we update the form to reflect the results
	        
	 //    }
	</script>
</body>