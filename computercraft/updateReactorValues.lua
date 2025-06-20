local DefineSettings = require("lib.settings")

DefineSettings()
settings.load()

-- Load settings
local FluxInputId = tostring(settings.get("draco.input_flowgate_id"))
local FluxOutputId = tostring(settings.get("draco.output_flowgate_id"))

-- Wrap peripherals
local FluxGateInjector = peripheral.wrap("flow_gate_" .. FluxInputId)
local FluxGateOutput = peripheral.wrap("flow_gate_" .. FluxOutputId)

-- API endpoint
local API_URL = "http://192.168.106.210:8080/reactor/1"

-- Tolerance value
local TOLERANCE = 5000

-- Helper function to check if values differ significantly
local function isSignificantlyDifferent(current, target)
    return math.abs(current - target) > TOLERANCE
end

-- Validate peripherals
if not FluxGateInjector or not FluxGateOutput then
    print("Failed to wrap one or both Flux Gates. Check IDs or side connections.")
    return
end

-- Main loop
while true do
    local response = http.get(API_URL)
    if response then
        local body = response.readAll()
        response.close()

        local data = textutils.unserializeJSON(body)
        local newInput = tonumber(data.inputValue)
        local newOutput = tonumber(data.outputvalue)

        if newInput and FluxGateInjector.getSignalLowFlow then
            local currentInput = FluxGateInjector.getSignalLowFlow()
            if isSignificantlyDifferent(currentInput, newInput) then
                FluxGateInjector.setSignalLowFlow(newInput)
                print("Updated FluxGateInjector from " .. currentInput .. " to " .. newInput)
            end
        end

        if newOutput and FluxGateOutput.getSignalLowFlow then
            local currentOutput = FluxGateOutput.getSignalLowFlow()
            if isSignificantlyDifferent(currentOutput, newOutput) then
                FluxGateOutput.setSignalLowFlow(newOutput)
                print("Updated FluxGateOutput from " .. currentOutput .. " to " .. newOutput)
            end
        end
    else
        print("Failed to fetch reactor data from API.")
    end

    sleep(1)
end
