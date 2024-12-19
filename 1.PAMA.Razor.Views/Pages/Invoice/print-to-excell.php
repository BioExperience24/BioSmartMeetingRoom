<?php

function rupiah($angka){
    $hasil_rupiah = "Rp " . number_format($angka,0,',','.');
    return $hasil_rupiah;
}
$ppn = ($invoice['total_cost']-0) * 0.1;
$total = $ppn + ( $invoice['total_cost']-0);
?>
<html xmlns:x="urn:schemas-microsoft-com:office:excel">
    <head>
        <xml>
                    <x:ExcelWorkbook>
                        <x:ExcelWorksheets>
                            <x:ExcelWorksheet>
                                <x:Name>Sheet 1</x:Name>
                                <x:WorksheetOptions>
                                    <x:Print>
                                        <x:ValidPrinterInfo/>
                                    </x:Print>
                                </x:WorksheetOptions>
                            </x:ExcelWorksheet>
                        </x:ExcelWorksheets>
                    </x:ExcelWorkbook>
                </xml>
        <title>Print To Excell</title>
        <style>
            body{
                width: 1000px;
                /*background: red;*/
            }
        </style>
    </head>
    <body>
        <br><br><br>
		<table style="width: 100%;" border="0">
			<tr><td></td>
				<td align="center" style="background: yellow; padding-top: 20px;" ><h1>INVOICE - <?= @$invoice['no_invoice']?></h1></td>
			</tr>
		</table>
		<br>
		<table border="0" style="width: 1000px;">
			<tr>
				<td style="width: 50%;">
					<table>
						<tr>
							<td>Kpd. </td>
							<td><?= $invoice['alocation_name']?></td>
						</tr>
						<tr>
							<td></td>
							<td></td>
						</tr>
						<tr>
							<td>Up.</td>
							<td></td>
						</tr>
						<tr>
							<td></td>
							<td>Jabatan</td>
						</tr>
					</table>
				</td>
				<td style="width: 50%;">
					<table>
						<tr>
							<td>Tanggal</td>
							<td><?= date("Y-m-d", strtotime(@$invoice['created_at']))?></td>
						</tr>
						<tr>
							<td>No. Invoice</td>
							<td><?= $invoice['no_invoice']?> </td>
						</tr>
						<tr>
							<td>No. Profit Center</td>
							<td></td>
						</tr>
					</table>
				</td>
			</tr>
		</table> 
		<br>     
		<table border="1" style="width: 1000px;">
			<tr>
				<td align="center" style="width: 60%;"><h1>Uraian</h1></td>
				<td align="center" style="width: 40%;"><h1>Jumlah</h1></td>
			</tr>
			<tr>
				<td align="center" >
					<p align="left" style="padding-left:20%;">
						Pembayaran atas pemakaian ruang meeting, <br>		
						Sesuai dengan Kontrak/ Perjanjian No………..<br>		
						<br>	
						Rincian terlampir.<br>	
						<br>	
						Periode Bulan Januari s/d  Desember Tahun : <?= @$invoice['invoice_years']?>	<br>	
								
					</p>
				</td>
				<td style="padding-left:20%;"><h1></h1></td>
			</tr>
			<tr>
				<td align="center" >
					<p align="left" style="padding-left:20%;">
						<b>Nilai Tagihan</b>
					</p>
				</td>
				<td style=""><b><?= @rupiah(@$invoice['total_cost'])?> </b></td>
			</tr>
			<tr>
				<td align="center" >
					<p align="left" style="padding-left:20%;">
						<b>PPN 10%</b>
					</p>
				</td>
				<td style=""> <b><?= @rupiah($ppn)?> </b> </td>
			</tr>
			<tr valign="top">
				<td align="center" valign="top" style="padding-top: 20px;" >
					<h3>TOTAL</h3>
				</td>
				<td style="padding-top: 20px;"><b><?= @rupiah($total)?> </b></td>
			</tr>
		</table> 
		<br>
		<table border="0" style="width: 1000px;">
			<tr>
				<td  style="width: 60%;"><b>Terbilang:</b></td>
				<td  style="width: 40%;"> <b></b> </td>
			</tr>
			<tr>
				<td align="center" colspan="2">
					<p align="left" >
						<b>Mohon Tagihan tersebut dikirim pada : </b>
						PT. Bank Negara Indonesia - Cabang Jatinegara <br>	
						Rekening No. 0008912317 ( Rekening Rupiah ) <br>			
						Swift Code No. : BNINIDJA <br>			
						Atas Nama : PT. Rekayasa Industri <br>			
					</p>
				</td>
			</tr>
		</table> 
		<br>
		<table style="width: 100%;" border="0">
			<tr  valign="bottom">
				<td align="center" style="background: yellow; padding-top: 20px;"><h3>PT. Rekayasa Industri </h3></td>
			</tr>
		</table>
		<table style="width: 100%;" border="0">
			<tr>
				<td style="width: 40%;"></td>
				<td align="center" valign="middle">
					<p align="left" >
						<b style="background: yellow;">Note :	terlampir batasan nilai tagihan,  dapat disesuaikan dalam system</b>		<br>
						1 s/d 100 Juta 	Yuslimi Azmi (VP Finance)	<br>
						101 juta sd 1 Milyar	Dedy Rinaldi (SVP Finance, Accounting & Tax)	<br>
						Lebih dari 1 Milyar	Asep Sukma Ibrada (Finance Director) <br>
					</p>
				</td>
			</tr>
		</table>
		<br>
		<table style="width: 100%;" border="0">
			<tr>
				<td style="background: yellow;padding-top: 20px;" align="center" valign="middle"><h4>Dedy Rinaldi </h4></td>
			</tr>
		</table> 
    </body>  
</html>