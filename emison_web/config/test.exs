use Mix.Config

# Configure your database
config :emison_web, EmisonWeb.Repo,
  username: "postgres",
  password: "postgres",
  database: "emison_web_test",
  hostname: "localhost",
  pool: Ecto.Adapters.SQL.Sandbox

# We don't run a server during test. If one is required,
# you can enable the server option below.
config :emison_web, EmisonWebWeb.Endpoint,
  http: [port: 4002],
  server: false

# Print only warnings and errors during test
config :logger, level: :warn
