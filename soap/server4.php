<?php


require_once 'lib/nusoap.php';
	//* Função que será executada na chamada do metodo desse Web Service
	//* Soma (passagem de dois valores e retorno da soma desses valores)
	
	//echo ListaUf(true);
	//* Criando o WebService e Configurando
$namespace = "http://localhost:82/soap/server4.php";	

$server = new soap_server();
// configure our WSDL
$server->configureWSDL("HelloExample");
// set our namespace
$server->wsdl->schemaTargetNamespace = $namespace;

$server->soap_defencoding = 'utf-8';

//Register a method that has parameters and return types


//Register our method using the complex type
	$server->register(
        'GetProximo',
      	array('ID' => 'xsd:int'),
      	array('return' =>'xsd:string'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#GetProximo', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 
	
		$server->register(
        'GetDados',
      	array('ID' => 'xsd:int'),
      	array('return' =>'xsd:string'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#GetDados', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 
	
			$server->register(
        'GetStatus',
      	array('ID' => 'xsd:int'),
      	array('return' =>'xsd:boolean'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#GetStatus', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 
	
				$server->register(
        'IfUltimoLista',
      	array('ID' => 'xsd:int', 'inicio' => 'xsd:int', 'ultimo' => 'xsd:int'),
      	array('return' =>'xsd:boolean'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#IfUltimoLista', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 
	

	
	
	$server->register(
        'GetProcessoOpen',
      	array('ID' => 'xsd:int'),
      	array('return' =>'xsd:string'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#GetProcessoOpen', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 
	$server->register(
        'GetCaminhoProcess',
      	array('ID' => 'xsd:int'),
      	array('return' =>'xsd:string'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#GetCaminhoProcess', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 
	
$server->register(
        'InserirArq',
      	array('ID' => 'xsd:int', 'id_dado' => 'xsd:int', 'caminho' => 'xsd:string', 'status' => 'xsd:int'),
      	array('return' =>'xsd:boolean'),
    	  'urn:MyServicewsdl', 
        'urn:MyServicewsdl#InserirArq', 
        'rpc', 
        'literal',
        'Some comments about function 2'         
    ); 	
	

function GetProximo($ID) {
$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
		
		//$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";  
		//$result = mysql_fetch_array(mysql_query($sql));     
		//return $result['inicio_passo']."-".$result['fim_passo']." ; ".$result['data_inicio'];
					
					
		$query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =$ID ORDER BY `fim_passo` DESC LIMIT 1";
		$result = @ mysql_query($query);
		$dados =  @ mysql_fetch_assoc($result);
		$novoini =  (int)$dados["fim_passo"]+1;
		$novofim =  (int)$dados["fim_passo"]+100;
		
		$query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =$ID AND  `inicio_passo` =$novoini AND  `fim_passo` =$novofim ORDER BY  `fim_passo` DESC LIMIT 1";
		$result = @ mysql_query($query);
		$cont = @ mysql_num_rows($result);
	
		if($cont >= 1 ){
				while($cont >= 1){
					$query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =$ID ORDER BY `fim_passo` DESC LIMIT 1";
					$result = @ mysql_query($query);
					$dados = @ mysql_fetch_assoc($result);
					$novoini =  (int)$dados["fim_passo"]+1;
					$novofim =  (int)$dados["fim_passo"]+100;
					
					$query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =$ID AND  `inicio_passo` =$novoini AND  `fim_passo` =$novofim ORDER BY  `fim_passo` DESC LIMIT 1";
					$result = @ mysql_query($query);
					$cont = @ mysql_num_rows($result);
					if($cont == 0){
						if(!IfUltimoLista($ID, $novoini, $novofim)){
							$query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =$ID ORDER BY `fim_passo` DESC LIMIT 1";
							$result = @ mysql_query($query);
							$dados =  @ mysql_fetch_assoc($result);
							$novoini =  (int)$dados["fim_passo"]+1;
							$novofim =  (int)$dados["fim_passo"]+100;

					
							$query = "INSERT INTO `lista_de_processos_e_status` (`num_processo`, `nome_processo`, `status`, `erros`,`data_inicio`, `data_final`, `caminho_comandos`, `caminho_salvar`, `arquivo_config_processo`, `inicio_passo`, `fim_passo`, `num_arq_processados`) VALUES (NULL, 'teste', '2', '0', '2013-11-25', '2013-11-26', '', '', '$ID', '$novoini', '$novofim', '100');";
							if(mysql_query($query)){
								return $novoini." ; ".$novofim." ;"	;
							  //return $query;
							}
						}else{
			   				if($novofim == 0 && $novoini == 0){
								if(Closeprocesso($ID)){
									return "0 ; 0 ;";
								}					
							} else {
								
								$query = "INSERT INTO `lista_de_processos_e_status` (`num_processo`, `nome_processo`, `status`, `erros`,`data_inicio`, `data_final`, `caminho_comandos`, `caminho_salvar`, `arquivo_config_processo`, `inicio_passo`, `fim_passo`, `num_arq_processados`) VALUES (NULL, 'teste', '2', '0', '2013-11-25', '2013-11-26', '', '', '$ID', '$novoini', '$novofim', '100');";
								if(mysql_query($query)){
									return $novoini." ; ".$novofim." ;"	;
								  //return $query;
								}
							}
			 			}
					}
				}
		}else{
			 if(!IfUltimoLista($ID, $novoini, $novofim)){
				   $query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =$ID ORDER BY `fim_passo` DESC LIMIT 1";
					$result = @ mysql_query($query);
					$dados =  @ mysql_fetch_assoc($result);
					$novoini =  (int)$dados["fim_passo"]+1;
					$novofim =  (int)$dados["fim_passo"]+100;
					
				   $query = "INSERT INTO `lista_de_processos_e_status` (`num_processo`, `nome_processo`, `status`, `erros`,`data_inicio`, `data_final`, `caminho_comandos`, `caminho_salvar`, `arquivo_config_processo`, `inicio_passo`, `fim_passo`, `num_arq_processados`) VALUES (NULL, 'teste', '2', '0', '2013-11-25', '2013-11-26', NULL, '', '$ID', '$novoini', '$novofim', '100');";
					if(mysql_query($query)){
					  return $novoini." ; ".$novofim." ; ";
					
					}
			 }else{
				if($novofim == 0 && $novoini == 0){
					if(Closeprocesso($ID)){
						return "0 ; 0 ;";
					}					
				} else {
								$query = "INSERT INTO `lista_de_processos_e_status` (`num_processo`, `nome_processo`, `status`, `erros`,`data_inicio`, `data_final`, `caminho_comandos`, `caminho_salvar`, `arquivo_config_processo`, `inicio_passo`, `fim_passo`, `num_arq_processados`) VALUES (NULL, 'teste', '2', '0', '2013-11-25', '2013-11-26', '', '', '$ID', '$novoini', '$novofim', '100');";
								if(mysql_query($query)){
									return $novoini." ; ".$novofim." ;"	;
								  //return $query;
								}
				}
			    
			}
	
		}
		
		return $novoini." ; ".$novofim." ; ";
		
	//$cont = @ mysql_num_rows($result);
	}
		
//return 0;
}
	//return $mycomplextype;	

 //$mycomplextype


$query = "INSERT INTO `sistemagerador`.`lista_de_processos_e_status` (`num_processo`, `nome_processo`, `status`, `erros`, `data_inicio`, `data_final`, `caminho_comandos`, `caminho_salvar`, `arquivo_config_processo`, `inicio_passo`, `fim_passo`, `num_arq_processados`) VALUES (NULL, 'teste', '2', '0', '2013-11-25', '2013-11-26', NULL, 'C:\\', '1', '1', '100', '100');";

$query = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";
			
		
			
	return false;
} 	

//echo GetProximo(1);
//echo "4".GetProximo(1);


function GetProcessoOpen($ID) {
	$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

    if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
	  //$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";  
			
		$sql = "SELECT * FROM  `arquivos_de_configuracao_processos` WHERE  `situacao` =1 ORDER BY data_arquivo DESC";	
		
		
		//$sql="SELECT * FROM  `configuracoes_de_processo` WHERE  `status_processo` =1 AND `id_confi_processo` =".$result['id_confi_processo'];
      	
			if(mysql_num_rows(mysql_query($sql)) >= 1){ 
			$result = mysql_fetch_array(mysql_query($sql));   
					return $result['id_arquivo'];
			}
		}
	}
	return "0";
} 
//echo GetProcessoOpen(0);

function GetCaminhoProcess($ID) {
	$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
	  //$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";  
			
		$sql = "SELECT * FROM  `arquivos_de_configuracao_processos` WHERE  `id_arquivo` = $ID ";	
		
		
		//$sql="SELECT * FROM  `configuracoes_de_processo` WHERE  `status_processo` =1 AND `id_confi_processo` =".$result['id_confi_processo'];
      	
			if(mysql_num_rows(mysql_query($sql)) >= 1){ 
			$result = mysql_fetch_array(mysql_query($sql));   
					return $result['destino_resultado'];
			}
		}
	}
	return "C:\\";
} 

function GetStatus($ID) {
	$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
      //$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";
	  $sql = "SELECT * FROM  `configuracoes_de_processo` WHERE  `id_confi_processo` =$ID AND  `status_processo` =1 LIMIT 0 , 300";  
			$result = mysql_fetch_array(mysql_query($sql)); 
			$cont = @ mysql_num_rows($result);
			if($cont >= 1){
				return true;
			}    
			
		}
	}
	return false;
} 

function IfUltimoLista($ID, $inicionovo, $ultimonovo) {
	$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
			$inicionovo1 = (int)$inicionovo-1; 
      //$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";
	        			
			$sql3 = "SELECT * FROM  `configuracoes_de_processo` WHERE  `id_confi_processo` =$ID LIMIT 0 , 300";  
			$result3 = mysql_fetch_array(mysql_query($sql3));    
			$cont3 = @ mysql_num_rows($result3);
			
			if(((int)$result3['Limite_total_de_registros'] >= $ultimonovo) and ((int)$result3['Limite_total_de_registros'] >= ($inicionovo1+100))){
				return false;
			}else{
				return true;
			} 
		}
	}
	
} 

function Closeprocesso($ID) {
	$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
	  //$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";  
			
		$sql = "UPDATE `arquivos_de_configuracao_processos` SET  `situacao` =  '2' WHERE `id_arquivo` = $ID;";	
		
		//$sql="SELECT * FROM  `configuracoes_de_processo` WHERE  `status_processo` =1 AND `id_confi_processo` =".$result['id_confi_processo'];
      	//$result2 = mysql_fetch_array(mysql_query($sql));     
			if(mysql_query($sql)){
			return true;
			}
		}
	}
	return false;
} 

function InserirArq($ID,$id_dado,$caminho, $status) {
	$connect = mysql_pconnect("mysql.hostinger.com.br","u267145878_natan","*nat2701");

if ($connect) {    
	   
	if(mysql_select_db("u267145878_serv", $connect)) {
			//$inicionovo1 = (int)$inicionovo-1; 
      //$sql = "SELECT * FROM  `lista_de_processos_e_status` WHERE  `arquivo_config_processo` =1 ORDER BY `fim_passo` DESC LIMIT 1";
	        			
			$sql3 = "INSERT INTO `arquivos_processados` (`id_arquivo_processo`, `id_dado`,`id_processo`, `data_criacao`, `data_recebi`, `tipo_arquivo`, `origem_arquivo`, `destino_final`, `paginas_deste`, `erros`, `situacao`) VALUES (NULL, '$id_dado', '$ID' , '".date("Y-m-d")."', '', '', NULL, '$caminho', NULL, NULL, '$status');";  
			//$result3 = mysql_fetch_array(mysql_query($sql3));    
			//$cont3 = @ mysql_num_rows($result3);
			
			if(mysql_query($sql3)){
				return true;
			}else{
				return false;
			} 
		}
	}
	
} 


//echo IfUltimoLista(1,2000,2100);
// pass our posted data (or nothing) to the soap service                    
$HTTP_RAW_POST_DATA = isset($HTTP_RAW_POST_DATA) ? $HTTP_RAW_POST_DATA : '';

$server->service($HTTP_RAW_POST_DATA);

exit(); 


?>

