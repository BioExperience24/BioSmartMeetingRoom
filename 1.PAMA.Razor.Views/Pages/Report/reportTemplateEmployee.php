<?php  
// error_reporting(0);
?>
<!doctype html>
<html>
<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title></title>
	<link rel="icon" href="/images/favicon.png" type="image/x-icon">
	<style>
	body{
		font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
		text-align:center;
		color:#777;
	}
	
	body h1{
		font-weight:300;
		margin-bottom:0px;
		padding-bottom:0px;
		color:#000;
	}
	
	body h3{
		font-weight:300;
		margin-top:10px;
		margin-bottom:20px;
		font-style:italic;
		color:#555;
	}
	body h4{
		font-weight:bold;
		margin-top:10px;
		margin-bottom:20px;
		/*font-style:italic;*/
		color:#555;
		text-align: left;
	}
	
	body a{
		color:#06F;
	}
	
	.invoice-box{
		max-width:800px;
		/*width: 100%;*/
		/*margin:auto;
		padding:30px;
		border:1px solid #eee;
		box-shadow:0 0 10px rgba(0, 0, 0, .15);
		font-size:16px;
		line-height:24px;
		font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
		color:#555;*/
	}
	.invoice-box table{
		width: 100%;
	}
	.invoice-box table td{
		vertical-align:top;
		/*width: 100%;*/
	}
	.invoice-box table tr td:nth-child(2){
		/*text-align:right;*/
	}
	.invoice-box table tr.top table td.title{
		font-size:45px;
		line-height:45px;
		color:#333;
	}
	.inserTable {
		width: 100%;
		text-align:center;
	}
	</style>
</head>

<body>
	<h1>REPORT MEETING</h1>
	<h3>Date <?= $start?> To <?= $end?> </h3>
	<!-- Find the code on <a href="https://github.com/sparksuite/simple-html-invoice-template">GitHub</a>. Licensed under the <a href="https://opensource.org/licenses/MIT" target="_blank">MIT license</a>.<br><br><br> -->
	<div class="invoice-box">
		<table cellpadding="0" cellspacing="0">
				<tr class="top">
					<td colspan="2">
						<table>
							<tr>
								<td class="title">
									<img src="<?= $_SERVER["DOCUMENT_ROOT"]?>/SMR_WEB/assets/logo_rekin.png" style="width:100px; height: auto;">
								</td>
								<td style="text-align:right;">
									<!-- Invoice #: 123<br> -->
									Created Date: <?= date('d M Y')?><br>
									<br>
									
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
	</div>
	<br>
	<br>
	<?php foreach ($booking as $key => $value): ?>
		<div class="invoice-box">
			<table cellpadding="0" cellspacing="0">
				<tr class="top">
					<td colspan="2">
						<table>
							<tr>
								<td class="">
									Name: <?= $value['name']?><br>
									Division: <?= $value['division_id']?><br>
								</td>
								<td style="text-align:right;">
									<!-- Invoice #: 123<br> -->
									
									
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<h4>Meeting List</h4>
			<table style="width: 100%;" >
							<tr style="border-bottom: 1px solid #eee;font-weight: bold;">
								<td>Title</td>
								<td>Room</td>
								<td>Date</td>
								<td>Time</td>
								<td>Status Meeting</td>
							</tr>
							<tbody>
							<?php foreach ($value['booking'] as $kk => $vv): ?>
								<?php  
									$stts = "";
									if($vv['is_canceled'] == 1){
										$stts = "Canceled";
									}

								?>
								<tr>
									<td><?= $vv['title']?></td>
									<td><?= $vv['room_name']?></td>
									<td><?= date("d M Y", strtotime($vv['date']))?></td>
									<td><?= $stts?></td>
								</tr>
							<?php endforeach ?>
								
							</tbody>
						</table>
			
		</div>
	
	<br>
	<br>
	<hr>
	<?php endforeach ?>
	
<!-- <script data-cfasync="false" src="/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js"></script></body> -->
</html>
