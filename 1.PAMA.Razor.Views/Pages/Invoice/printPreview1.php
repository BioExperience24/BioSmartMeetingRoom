<?php
function rupiah($angka){
	
	$hasil_rupiah = "Rp " . number_format($angka,0,',','.');
	return $hasil_rupiah;
 
}

?>	

<!doctype html>
<html>
<head>
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	
	<title></title>
	
	
	<!-- Favicon -->
	<link rel="icon" href="/images/favicon.png" type="image/x-icon">
	
	
	<!-- Invoice styling -->
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
	
	body a{
		color:#06F;
	}
	
	.invoice-box{
		max-width:800px;
		margin:auto;
		padding:30px;
		border:1px solid #eee;
		box-shadow:0 0 10px rgba(0, 0, 0, .15);
		font-size:16px;
		line-height:24px;
		font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
		color:#555;
	}
	
	.invoice-box table{
		width:100%;
		/*line-height:inherit;*/
		text-align:left;
	}
	
	.invoice-box table td{
		padding:5px;
		vertical-align:top;
	}
	
	.invoice-box table tr td:nth-child(2){
		text-align:right;
	}
	
	.invoice-box table tr.top table td{
		padding-bottom:20px;
	}
	
	.invoice-box table tr.top table td.title{
		font-size:45px;
		line-height:45px;
		color:#333;
	}
	
	.invoice-box table tr.information table td{
		padding-bottom:40px;
	}
	
	.invoice-box table tr.heading td{
		background:#eee;
		border-bottom:1px solid #ddd;
		font-weight:bold;
	}
	
	.invoice-box table tr.details td{
		padding-bottom:20px;
	}
	
	.invoice-box table tr.item td{
		border-bottom:1px solid #eee;
	}
	
	.invoice-box table tr.item.last td{
		border-bottom:none;
	}
	
	.invoice-box table tr.total td:nth-child(2){
		border-top:2px solid #eee;
		font-weight:bold;
	}
	
	/*@media only screen and (max-width: 600px) {
		.invoice-box table tr.top table td{
			width:100%;
			display:block;
			text-align:center;
		}
		
		.invoice-box table tr.information table td{
			width:100%;
			display:block;
			text-align:center;
		}
	}*/
	</style>
</head>

<body>
	<h1>Invoice</h1>
	<h3></h3>
	<!-- Find the code on <a href="https://github.com/sparksuite/simple-html-invoice-template">GitHub</a>. Licensed under the <a href="https://opensource.org/licenses/MIT" target="_blank">MIT license</a>.<br><br><br> -->
	
	<div class="invoice-box">
		<table cellpadding="0" cellspacing="0">
			<tr class="top">
				<td colspan="2">
					<table>
						<tr>
							<td class="title">
								<img src="<?= $_SERVER["DOCUMENT_ROOT"]?>/SMR_WEB/assets/logo_rekin.png" style="width:120px; max-width:300px;">
							</td>
							
							<td>
				
								Invoice #: <?= rand(100,999)?><br>
								Created: <?= date("d M Y")?><br>
								<!-- Due: February 1, 2015 -->
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
								Jl. Kalibata Timur I No. 36<br>
								Kalibata Jakarta 12740, <br>
								Indonesia
							</td>
							
							<td>
								PT REKAYASA INDUSTRI .<br>
								Title: <?= $booking['title']?><br>
								Room: <?= $booking['room_name']?><br>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			
			
			<tr class="heading">
				<td>
					Item
				</td>
				
				<td>
					Price
				</td>
			</tr>
			
			<tr class="item">
				<td>
					Meeting
				</td>
				
				<td>
					<?= rupiah($price)?> 
				</td>
			</tr>
			
			<tr class="item">
				<td>
					Room Price
				</td>
				
				<td>
					<?= rupiah(200000)?> 
				</td>
			</tr>
			
			<!-- <tr class="item last">
				<td>
					Facility name (1 year)
				</td>
				
				<td>
					$10.00
				</td>
			</tr> -->
			
			<tr class="total">
				<td>
					<b>Total</b>
				</td>
				
				<td>
				   <?= rupiah($price+200000)?> 
				</td>
			</tr>
		</table>
	</div>
<script data-cfasync="false" src="/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js"></script></body>
</html>
