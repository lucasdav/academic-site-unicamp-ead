window.onload = function () 
{
      
}

function direcionarPortal() 
{
    window.location.href = "portal-aluno/portal-aluno.aspx";   
}    


$('.your-checkbox').prop('indeterminate', true)

$(document).ready(function() {
    
    $("span").hide();
    
    $("input[type='text']").blur(function(){
        
        if ($(this).val().length == 0) {
            
            $(this).next().show();
        }
        
        $("input[type='password']").blur(function(){
            
            if ($(this).val().length == 0) {
                
                $(this).next().show();
            }
            
            $("input[type='checkbox']").blur(function(){
                
                if ($(this).val().length == 0) {
                    
                    $(this).next().show();
                }
                
                $("input[type='date']").blur(function(){
                    
                    if ($(this).val().length == 0) {
                        
                        $(this).next().show();
                    }
                });
            });
            
            $("input[type='text']").focus(function() {
                $(this).next().hide();
            });  
        })
    })
});

