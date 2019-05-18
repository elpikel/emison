# This file is responsible for configuring your application
# and its dependencies with the aid of the Mix.Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
use Mix.Config

config :emison_web,
  ecto_repos: [EmisonWeb.Repo]

# Configures the endpoint
config :emison_web, EmisonWebWeb.Endpoint,
  url: [host: "localhost"],
  secret_key_base: "EZ76NwQgnaJckmogAL9wr0epjCSgoCr7GLwIKtd6gmMgSACFQV1Ui2JSlkImAC16",
  render_errors: [view: EmisonWebWeb.ErrorView, accepts: ~w(html json)],
  pubsub: [name: EmisonWeb.PubSub, adapter: Phoenix.PubSub.PG2]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"
