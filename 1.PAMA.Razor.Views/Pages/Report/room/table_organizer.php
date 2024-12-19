<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Preview</title>
    <!-- Bootstrap Core Css -->
    <link href="<?php echo base_url(" assets/theme/plugins/bootstrap/css/bootstrap.css") ?>" rel="stylesheet">
</head>

<body>
	<button onclick="ExportToExcel('xlsx')">Export To Excel</button>
    <table id="tbl_exporttable_to_xls" border="1">
    	<tr>
        	<td colspan="8" height="100"></td>
        </tr>
        <tr>
        	<td colspan="1">Report :</td>
        	<td colspan="2">Meeting Organizer</td>
        	<td colspan="1"></td>
        	<td colspan="1">Period</td>
        	<td colspan="2"><?= $report['period']?></td>
        </tr>
        <tr>
        	<td colspan="1">Employee :</td>
        	<td colspan="2"><?= $report['employee']?></td>
        	<td colspan="1"></td>
        	<td colspan="1">Building</td>
        	<td colspan="2"><?= $report['building']?></td>
        </tr>
        <tr>
        	<td colspan="1">Room :</td>
        	<td colspan="2"><?= $report['room']?></td>
        	<td colspan="4"></td>
        	
        </tr>
        <tr>
        	<td colspan="8" height="100"></td>
        </tr>
       
        <tr>
        	<td></td>
            <td>Name</td>
            <td>Company&Department</td>
            <td>Meeting</td>
            <td>Reschedule</td>
            <td>Cancel</td>
            <td>Total Duration</td>
            <td>Attendees</td>
            <?php if ($report['modules']['room_adv']['is_enabled'] == 1): ?>
            <td>Attendees Check-in</td>
            <td>Auto Release</td>
            <td>Approve</td>
            <td>Reject</td>
            <?php else: ?>
            <?php endif;?>
        </tr>
        <?php foreach ($report['data'] as $key => $value): ?>
        	<tr>
        	<td></td>
            <td><?= $value['name']?><br><small><?= $value['nik_display']?></small></td>
            <td><?= $value['company_name']?><br><small><b><?= $value['department_name']?></b></small></td>
            <td><?= $value['total_meeting']?></td>
            <td><?= $value['total_reschedule']?></td>
            <td><?= $value['total_cancel']?></td>
            <td><?= $value['total_duration']?></td>
            <td><?= $value['total_attendees']?></td>
            <?php if ($report['modules']['room_adv']['is_enabled'] == 1): ?>
            <td><?= $value['total_attendees_checkin']?></td>
            <td><?= $value['total_duration']?></td>
            <td><?= $value['total_approve']?></td>
            <td><?= $value['total_reject']?></td>
            <?php else: ?>
            <?php endif;?>
        </tr>
       	
       	<?php endforeach ?>
            
        	
    </table>
    <!-- Jquery Core Js -->
    <script src="<?php echo base_url(" assets/theme/plugins/jquery/jquery.min.js");?>"> </script> 
    <script src = "<?php echo base_url("assets / theme / plugins / bootstrap / js / bootstrap.js ");?>" >
    </script>
    <!-- Slimscroll Plugin Js -->
    <script src="<?php echo base_url(" assets/theme/plugins/jquery-slimscroll/jquery.slimscroll.js");?>"> </script> <!-- Waves Effect Plugin Js -->
    <script src = "<?php echo base_url("
    assets / theme / plugins / node - waves / waves.js ");?>" >
    </script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment-timezone.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment-timezone-data.min.js"></script>
    <!-- Jquery CountTo Plugin Js -->
    <script src="<?= base_url()?>assets/external/sheetjs/xlsx.full.min.js"></script>
    <script type="text/javascript">
    	function ExportToExcel(type, fn, dl) {
    		var namefile = moment().format("X")+"_Report_Meeting_organizer.xlsx";
	       	var elt = document.getElementById('tbl_exporttable_to_xls');
	       	var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
	       	return dl ?
	        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }):
	        XLSX.writeFile(wb, namefile);
	    }
    </script>
</body>

</html>