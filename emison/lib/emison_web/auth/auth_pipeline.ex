defmodule EmisonWeb.Guardian.AuthPipeline do
    use Guardian.Plug.Pipeline, otp_app: :emison,
    module: Emison.Guardian,
    error_handler: EmisonWeb.AuthErrorHandler
  
    plug Guardian.Plug.VerifyHeader, realm: "Bearer"
    plug Guardian.Plug.EnsureAuthenticated
    plug Guardian.Plug.LoadResource
end