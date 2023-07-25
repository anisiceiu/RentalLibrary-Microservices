
function setAvtiveMen(e)
{
    var allMenu=document.querySelectorAll('.nav-link');
    allMenu.forEach(ele=>{

        ele.classList.remove('active');
    });

   if(e)
   {
    e.classList.add('active');
   }
}  