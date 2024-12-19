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
		text-align:right;
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
	<?php foreach ($booking as $key => $value): ?>
		<div class="invoice-box">
			<table cellpadding="0" cellspacing="0">
				<tr class="top">
					<td colspan="2">
						<table>
							<tr>
								<td class="title">
									<img src="<?= $_SERVER["DOCUMENT_ROOT"]?>/SMR_WEB/assets/logo_rekin.png" style="width:100px; height: auto;">
								</td>
								
								<td>
									<!-- Invoice #: 123<br> -->
									Date: <?= date("d M Y", strtotime($value['date']))?><br>
									Time: <?= date("H:i", strtotime($value['start']))?> - <?= date("H:i", strtotime($value['end']))?><br>
									<!-- Time Finish: <?= date("H:i", strtotime($value['end']))?> <br> -->
									
								</td>
							</tr>
						</table>
					</td>
				</tr>
				
				<tr class="information">
					<td colspan="2">
						<table>
							<tr>
								<td>
									<!-- Invoice #: 123<br> -->
									Title Meeting: <b><?= $value['title']?></b><br>
									Room: <b><?= $value['room_name']?> </b><br>
									Total Invitation: <b><?= count($value['invitation_internal'])+count($value['invitation_eksternal']) ?> </b><br>
								</td>
								
								<td>
									PIC<br>
									<?= $value['pic']?><br>
									<!-- <a href="/cdn-cgi/l/email-protection" class="__cf_email__" data-cfemail="9ff5f0f7f1dffae7fef2eff3fab1fcf0f2">[email&#160;protected]</a> -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
				
			</table>
			<h4>Internal Invitation</h4>
			<table style="width: 100%;" >
							<tr style="border-bottom: 1px solid #eee;font-weight: bold;">
								<td>Name</td>
								<td>Division</td>
							</tr>
							<tbody>
							<?php foreach ($value['invitation_internal'] as $kk => $vv): ?>
								<tr>
									<td><?= $vv['name']?></td>
									<td><?= $vv['division_id']?></td>
								</tr>
							<?php endforeach ?>
								
							</tbody>
						</table>
			<h4>External Invitation</h4>
			<table>
							<tr style="border-bottom: 1px solid #eee;font-weight: bold;">
								<td>Name</td>
								<td>Company/Organzation</td>
							</tr>
							<tbody>
							<?php foreach ($value['invitation_eksternal'] as $kk => $vv): ?>
								<tr>
									<td><?= $vv['name']?></td>
									<td><?= $vv['company']?></td>
								</tr>
							<?php endforeach ?>
								
							</tbody>
						</table>
		</div>
	<br>
	<?php endforeach ?>
	<div class="invoice-box">
		<!-- lakshdlksdhlkajsd -->
	</div>
<!-- <script data-cfasync="false" src="/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js"></script></body> -->
</html>
