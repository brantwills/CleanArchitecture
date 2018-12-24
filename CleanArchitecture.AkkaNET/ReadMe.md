# Akka.Net

Using akka.net to handle all the event source work

## Sql Server Persistance

https://github.com/akkadotnet/Akka.Persistence.SqlServer

```
CREATE TABLE {your_journal_table_name} (
  Ordering BIGINT IDENTITY(1,1) NOT NULL,
  PersistenceID NVARCHAR(255) NOT NULL,
  SequenceNr BIGINT NOT NULL,
  Timestamp BIGINT NOT NULL,
  IsDeleted BIT NOT NULL,
  Manifest NVARCHAR(500) NOT NULL,
  Payload VARBINARY(MAX) NOT NULL,
  Tags NVARCHAR(100) NULL,
  SerializerId INTEGER NULL
	CONSTRAINT PK_{your_journal_table_name} PRIMARY KEY (Ordering),
  CONSTRAINT QU_{your_journal_table_name} UNIQUE (PersistenceID, SequenceNr)
);

CREATE TABLE {your_snapshot_table_name} (
  PersistenceID NVARCHAR(255) NOT NULL,
  SequenceNr BIGINT NOT NULL,
  Timestamp DATETIME2 NOT NULL,
  Manifest NVARCHAR(500) NOT NULL,
  Snapshot VARBINARY(MAX) NOT NULL,
  SerializerId INTEGER NULL
  CONSTRAINT PK_{your_snapshot_table_name} PRIMARY KEY (PersistenceID, SequenceNr)
);

CREATE TABLE {your_metadata_table_name} (
  PersistenceID NVARCHAR(255) NOT NULL,
  SequenceNr BIGINT NOT NULL,
  CONSTRAINT PK_{your_metadata_table_name} PRIMARY KEY (PersistenceID, SequenceNr)
);
```