defmodule Emison.Application do
  # See https://hexdocs.pm/elixir/Application.html
  # for more information on OTP Applications
  @moduledoc false

  use Application

  def start(_type, _args) do
    # List all child processes to be supervised
    children = [
      # Start the Ecto repository
      Emison.Repo,
      # Start the endpoint when the application starts
      EmisonWeb.Endpoint
      # Starts a worker by calling: Emison.Worker.start_link(arg)
      # {Emison.Worker, arg},
    ]

    # See https://hexdocs.pm/elixir/Supervisor.html
    # for other strategies and supported options
    opts = [strategy: :one_for_one, name: Emison.Supervisor]
    Supervisor.start_link(children, opts)
  end

  # Tell Phoenix to update the endpoint configuration
  # whenever the application is updated.
  def config_change(changed, _new, removed) do
    EmisonWeb.Endpoint.config_change(changed, removed)
    :ok
  end
end
