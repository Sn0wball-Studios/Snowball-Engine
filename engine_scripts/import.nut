local imported = [];

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

    if(imported.find(modulePath) == null)
    {
        imported.append(modulePath);
    }else
    {
        return;
    }

    local str = format("loaded module %s", modulePath)
    // sprint(str)

    modenv.dofile(modulePath);

    foreach (key, value in modenv) {
        rootenv.rawset(key, value);
    }

	setroottable(rootenv)
}
