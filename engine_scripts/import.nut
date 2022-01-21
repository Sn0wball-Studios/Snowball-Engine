function import(module) 
{
    local rootenv = getroottable();
    local modenv = clone rootenv;
    local modulePath = ""
    if(startswith(module, "snowball/"))
    {
        local t = split(module, "/");
       
        modulePath = "engine_scripts/" + t[1] + ".nut";
    }else
    {
        modulePath = "assets/scripts/" + module + ".nut";
    }

    modenv.dofile(modulePath);
	rootenv.rawset(module, modenv);

	setroottable(rootenv)
}
