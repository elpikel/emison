defmodule EmisonWeb.Repo do
  use Ecto.Repo,
    otp_app: :emison_web,
    adapter: Ecto.Adapters.Postgres
end
