defmodule Emison.Repo do
  use Ecto.Repo,
    otp_app: :emison,
    adapter: Ecto.Adapters.Postgres
end
