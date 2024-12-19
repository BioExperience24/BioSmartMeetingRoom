<?php  
$num = 0;

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
		<table id="tbldata_detail">
                <thead>
                    <th>ID</th>
                    <th>Title</th>
                    <th>PIC</th>
                    <th>Date</th>
                    <th>Time</th>
                    <th>Duration</th>
                    <th>Location</th>
                    <th>Alocation</th>
                </thead>
                <tbody>
					<?php foreach ($detail as $key => $value): ?>
						<?php $num++; ?>
						<?php  
							$timearea = date('h:i A', strtotime($value['start'])) . " " . date('h:i A', strtotime($value['end']));
							$times = ($value['total_duration']-0) + ($value['extended_duration']-0);
							$jam = $times/60;
						?>
						<tr>
							<td><?=  $value['booking_id']?></td>
							<td><?=  $value['title']?></td>
							<td><?=  $value['pic']?></td>
							<td><?=  $value['date']?></td>
							<td><?=  $timearea ?></td>
							<td><?=  $jam?> Hours</td>
							<td><?=  $value['location']?></td>
							<td><?=  $value['alocation_name']?></td>
						</tr>
					<?php endforeach ?>
                </tbody>
            </table>
			
		<br>     
		
    </body>  
</html>