local api_url = "http://192.168.106.210:8080/chain"

reactor = peripheral.wrap("right")
reactorID = 1

function sendReactorData()
    while true do
        local info = reactor.getReactorInfo()
        
        if info then
            local data = {
                temperature = info.temperature,
                fieldStrength = info.fieldStrength,
                energySaturation = info.energySaturation,
                fuelExhaustion = info.fuelConversion,
                ReactorId = reactorID
            }
            print(data)
            local jsonData = textutils.serialiseJSON(data)
            print("json data: " .. jsonData)
            local response, err = http.post(api_url, jsonData, { ["Content-Type"] = "application/json"})
            print(response)
            if response then
                print("Data sent successfully")
                response.close()
            else
                print("Failed to send data " .. (err or "Unknown error"))
            end
        end
        
        sleep(1)
    end
end

parallel.waitForAny(sendReactorData)
