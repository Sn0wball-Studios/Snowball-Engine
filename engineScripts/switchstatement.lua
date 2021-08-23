local switchstatement = {}


function switchstatement.switch(t)
    t.case = function (self,x)
      local f=self[x] or self.default
      if f then
        if type(f)=="function" then
          f(x,self)
        else
          error("case "..tostring(x).." not a function")
        end
      end
    end
    return t
end

return switchstatement