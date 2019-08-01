defmodule Emison.Repo.Migrations.CreateGreetings do
  use Ecto.Migration

  def change do
    create table(:greetings, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :text, :string
      add :image, :string
      add :wedding_id, references(:weddings, on_delete: :nothing, type: :binary_id)

      timestamps()
    end

    create index(:greetings, [:wedding_id])
  end
end
